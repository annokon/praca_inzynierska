import React, { useEffect, useMemo, useState } from "react";
import "../../css/list_page.css";
import "../../css/profile.css";
import "../../css/notifications_page.css";

const API_BASE_URL = "http://localhost:5292";
const NOTIFICATIONS_PER_PAGE = 6;

const NOTIFICATION_TABS = [
    {
        id: "general",
        label: "Ogólne powiadomienia",
        icon: (
            <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="2">
                <path d="M18 8a6 6 0 0 0-12 0c0 7-3 7-3 7h18s-3 0-3-7" />
                <path d="M13.73 21a2 2 0 0 1-3.46 0" />
            </svg>
        )
    },
    {
        id: "trips",
        label: "Podróże",
        icon: (
            <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="2">
                <path d="M22 2L11 13" />
                <path d="M22 2l-7 20-4-9-9-4 20-7z" />
            </svg>
        )
    }
];

function getNotificationId(notification) {
    return (
        notification?.id ??
        notification?.idNotification ??
        notification?.notificationId ??
        null
    );
}

function getNotificationTab(notification) {
    const value = (
        notification?.type ??
        notification?.category ??
        notification?.notificationType ??
        ""
    ).toString().toLowerCase();

    if (
        value.includes("trip") ||
        value.includes("travel") ||
        value.includes("podroz") ||
        value.includes("podróż")
    ) {
        return "trips";
    }

    return "general";
}

function getNotificationIsRead(notification) {
    if (typeof notification?.isRead === "boolean") return notification.isRead;
    if (typeof notification?.read === "boolean") return notification.read;
    if (typeof notification?.seen === "boolean") return notification.seen;
    if (typeof notification?.isSeen === "boolean") return notification.isSeen;

    if (notification?.readAt || notification?.read_at || notification?.seenAt) {
        return true;
    }

    return false;
}

function getNotificationTitle(notification) {
    return (
        notification?.title ??
        notification?.notificationTitle ??
        notification?.name ??
        "Powiadomienie"
    );
}

function getNotificationText(notification) {
    return (
        notification?.message ??
        notification?.content ??
        notification?.description ??
        notification?.text ??
        ""
    );
}

function formatNotificationDate(value) {
    if (!value) return "";

    const date = new Date(value);

    if (Number.isNaN(date.getTime())) {
        return value;
    }

    const today = new Date();
    const isToday = date.toDateString() === today.toDateString();

    if (isToday) {
        return date.toLocaleTimeString("pl-PL", {
            hour: "2-digit",
            minute: "2-digit"
        });
    }

    return date.toLocaleDateString("pl-PL", {
        day: "numeric",
        month: "long"
    });
}

function getNotificationTime(notification) {
    return (
        notification?.timeLabel ??
        notification?.dateLabel ??
        notification?.createdAtLabel ??
        formatNotificationDate(
            notification?.createdAt ??
            notification?.created_at ??
            notification?.date
        )
    );
}

function NotificationTabs({ activeTab, onTabChange }) {
    return (
        <div className="profile-tabs-bar notifications-tabs-bar">
            <div className="tabs notifications-tabs">
                {NOTIFICATION_TABS.map((tab) => (
                    <button
                        key={tab.id}
                        type="button"
                        className={`tab notification-tab ${activeTab === tab.id ? "is-active" : ""}`}
                        onClick={() => onTabChange(tab.id)}
                    >
                        {tab.icon}
                        {tab.label}
                    </button>
                ))}
            </div>
        </div>
    );
}

export default function NotificationsPage() {
    const [notifications, setNotifications] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [activeTab, setActiveTab] = useState("general");
    const [currentPage, setCurrentPage] = useState(1);

    useEffect(() => {
        const loadNotifications = async () => {
            setLoading(true);
            setError("");

            try {
                const res = await fetch(`${API_BASE_URL}/api/notifications`, {
                    method: "GET",
                    credentials: "include"
                });

                if (!res.ok) {
                    const errorText = await res.text();
                    console.error("Odpowiedź backendu:", errorText);
                    throw new Error("Błąd odpowiedzi z serwera");
                }

                const data = await res.json();

                const notificationsList = Array.isArray(data)
                    ? data
                    : data.notifications ?? data.items ?? data.data ?? [];

                setNotifications(Array.isArray(notificationsList) ? notificationsList : []);
                setCurrentPage(1);
            } catch (err) {
                console.error("Błąd pobierania powiadomień:", err);
                setNotifications([]);
                setError("Wystąpił błąd podczas pobierania powiadomień.");
            } finally {
                setLoading(false);
            }
        };

        loadNotifications();
    }, []);

    useEffect(() => {
        setCurrentPage(1);
    }, [activeTab]);

    const filteredNotifications = useMemo(() => {
        return notifications.filter((notification) => getNotificationTab(notification) === activeTab);
    }, [notifications, activeTab]);

    const totalPages = Math.max(
        1,
        Math.ceil(filteredNotifications.length / NOTIFICATIONS_PER_PAGE)
    );

    useEffect(() => {
        if (currentPage > totalPages) {
            setCurrentPage(totalPages);
        }
    }, [currentPage, totalPages]);

    const paginatedNotifications = useMemo(() => {
        const startIndex = (currentPage - 1) * NOTIFICATIONS_PER_PAGE;
        const endIndex = startIndex + NOTIFICATIONS_PER_PAGE;

        return filteredNotifications.slice(startIndex, endIndex);
    }, [filteredNotifications, currentPage]);

    const paginationItems = useMemo(() => {
        if (totalPages <= 5) {
            return Array.from({ length: totalPages }, (_, index) => index + 1);
        }

        if (currentPage <= 3) {
            return [1, 2, 3, "...", totalPages];
        }

        if (currentPage >= totalPages - 2) {
            return [1, "...", totalPages - 2, totalPages - 1, totalPages];
        }

        return [1, "...", currentPage, "...", totalPages];
    }, [currentPage, totalPages]);

    const goToPage = (page) => {
        if (page < 1 || page > totalPages) return;

        setCurrentPage(page);
        window.scrollTo({ top: 0, behavior: "smooth" });
    };

    return (
        <div className="page-shell">
            <div className="page-container notifications-container">
                <h1 className="page-title">
                    Powiadomienia
                </h1>

                <NotificationTabs
                    activeTab={activeTab}
                    onTabChange={setActiveTab}
                />

                {loading && (
                    <div className="page-info">
                        Ładowanie powiadomień...
                    </div>
                )}

                {!loading && error && (
                    <div className="page-info page-error">
                        {error}
                    </div>
                )}

                {!loading && !error && filteredNotifications.length === 0 && (
                    <div className="page-info">
                        Brak powiadomień w tej zakładce.
                    </div>
                )}

                {!loading && !error && filteredNotifications.length > 0 && (
                    <>
                        <div className="result-list notifications-list">
                            {paginatedNotifications.map((notification, index) => {
                                const notificationId = getNotificationId(notification);
                                const isRead = getNotificationIsRead(notification);

                                return (
                                    <div
                                        key={notificationId ?? index}
                                        className={`result-card notification-card ${
                                            isRead ? "notification-card-read" : "notification-card-unread"
                                        }`}
                                    >
                                        <div className="result-card-left">
                                            <span
                                                className="notification-dot"
                                                aria-hidden="true"
                                            />

                                            <div className="result-card-content">
                                                <div className="result-card-title">
                                                    {getNotificationTitle(notification)}
                                                </div>

                                                <div className="result-card-text">
                                                    {getNotificationText(notification)}
                                                </div>
                                            </div>
                                        </div>

                                        <div className="result-card-meta notification-time">
                                            {getNotificationTime(notification)}
                                        </div>
                                    </div>
                                );
                            })}
                        </div>

                        {totalPages > 1 && (
                            <div className="pagination">
                                <button
                                    type="button"
                                    className="pagination-btn"
                                    onClick={() => goToPage(currentPage - 1)}
                                    disabled={currentPage === 1}
                                >
                                    &lt;
                                </button>

                                {paginationItems.map((item, index) =>
                                    item === "..." ? (
                                        <span
                                            key={`dots-${index}`}
                                            className="pagination-dots"
                                        >
                                            ...
                                        </span>
                                    ) : (
                                        <button
                                            type="button"
                                            key={item}
                                            className={`pagination-btn ${currentPage === item ? "active" : ""}`}
                                            onClick={() => goToPage(item)}
                                        >
                                            {item}
                                        </button>
                                    )
                                )}

                                <button
                                    type="button"
                                    className="pagination-btn"
                                    onClick={() => goToPage(currentPage + 1)}
                                    disabled={currentPage === totalPages}
                                >
                                    &gt;
                                </button>
                            </div>
                        )}
                    </>
                )}
            </div>
        </div>
    );
}