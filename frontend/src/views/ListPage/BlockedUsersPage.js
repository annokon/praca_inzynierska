import React, { useEffect, useMemo, useState } from "react";
import { Link } from "react-router-dom";
import "../../css/list_page.css";
import { getProfileImageFromResponse, getImageSrc } from "../../components/utils/imageHelper";

const API_BASE_URL = "http://localhost:5292";

const BLOCKED_USERS_ENDPOINT = `${API_BASE_URL}/api/blocked-users`;
const USERS_PER_PAGE = 10;

function getUserId(user) {
    return user?.id ?? user?.idUser ?? user?.userId ?? null;
}

function getUserProfilePath(user) {
    const username = user?.username;

    if (username) {
        return `/profile/${username}/about-user`;
    }

    const id = getUserId(user);

    return `/profile/${id}/about-user`;
}

function BlockIcon({ isUnblocked }) {
    return (
        <svg
            viewBox="0 0 24 24"
            width="26"
            height="26"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
        >
            <circle
                cx="12"
                cy="12"
                r="8.5"
                stroke={isUnblocked ? "#9ca3af" : "#111827"}
                strokeWidth="1.8"
            />
            <path
                d="M6.5 17.5L17.5 6.5"
                stroke={isUnblocked ? "#9ca3af" : "#111827"}
                strokeWidth="1.8"
                strokeLinecap="round"
            />
        </svg>
    );
}

export default function BlockedUsersPage() {
    const [users, setUsers] = useState([]);
    const [profileImages, setProfileImages] = useState({});
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [actionError, setActionError] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const [changingBlockedUserIds, setChangingBlockedUserIds] = useState([]);
    const [unblockedUserIds, setUnblockedUserIds] = useState([]);

    useEffect(() => {
        const loadBlockedUsers = async () => {
            setLoading(true);
            setError("");
            setActionError("");

            try {
                const res = await fetch(BLOCKED_USERS_ENDPOINT, {
                    method: "GET",
                    credentials: "include"
                });

                if (!res.ok) {
                    const errorText = await res.text();
                    console.error("Odpowiedź backendu:", errorText);
                    throw new Error("Błąd odpowiedzi z serwera");
                }

                const data = await res.json();

                setUsers(Array.isArray(data) ? data : []);
                setCurrentPage(1);
                setProfileImages({});
                setUnblockedUserIds([]);
            } catch (err) {
                console.error("Błąd pobierania zablokowanych użytkowników:", err);
                setUsers([]);
                setError("Wystąpił błąd podczas pobierania zablokowanych użytkowników.");
            } finally {
                setLoading(false);
            }
        };

        loadBlockedUsers();
    }, []);

    const totalPages = Math.max(1, Math.ceil(users.length / USERS_PER_PAGE));

    useEffect(() => {
        if (currentPage > totalPages) {
            setCurrentPage(totalPages);
        }
    }, [currentPage, totalPages]);

    const paginatedUsers = useMemo(() => {
        const startIndex = (currentPage - 1) * USERS_PER_PAGE;
        const endIndex = startIndex + USERS_PER_PAGE;

        return users.slice(startIndex, endIndex);
    }, [users, currentPage]);

    useEffect(() => {
        const loadProfileImages = async () => {
            if (paginatedUsers.length === 0) return;

            const usersToLoad = paginatedUsers.filter((user) => {
                const userId = getUserId(user);

                return userId != null;
            });

            if (usersToLoad.length === 0) return;

            const results = await Promise.all(
                usersToLoad.map(async (user) => {
                    const userId = getUserId(user);

                    try {
                        const res = await fetch(
                            `${API_BASE_URL}/api/users/${userId}/images`,
                            {
                                method: "GET",
                                credentials: "include"
                            }
                        );

                        if (!res.ok) {
                            return [userId, ""];
                        }

                        const data = await res.json();
                        const profileImage = getProfileImageFromResponse(data);

                        return [userId, profileImage || ""];
                    } catch (err) {
                        console.error(`Błąd pobierania zdjęcia użytkownika ${userId}:`, err);
                        return [userId, ""];
                    }
                })
            );

            setProfileImages((prev) => {
                const next = { ...prev };

                results.forEach(([userId, profileImage]) => {
                    next[userId] = profileImage;
                });

                return next;
            });
        };

        loadProfileImages();
    }, [paginatedUsers]);

    const goToPage = (page) => {
        if (page < 1 || page > totalPages) return;

        setCurrentPage(page);
        window.scrollTo({ top: 0, behavior: "smooth" });
    };

    const handleToggleBlock = async (userId) => {
        if (userId == null) return;

        if (changingBlockedUserIds.includes(userId)) return;

        const isUnblocked = unblockedUserIds.includes(userId);
        const method = isUnblocked ? "POST" : "DELETE";

        setActionError("");
        setChangingBlockedUserIds((prev) => [...prev, userId]);

        try {
            const res = await fetch(`${BLOCKED_USERS_ENDPOINT}/${userId}`, {
                method,
                credentials: "include"
            });

            if (!res.ok) {
                const errorText = await res.text();
                console.error("Odpowiedź backendu:", errorText);
                throw new Error(
                    isUnblocked
                        ? "Błąd ponownego blokowania użytkownika"
                        : "Błąd odblokowywania użytkownika"
                );
            }

            if (isUnblocked) {
                setUnblockedUserIds((prev) =>
                    prev.filter((id) => id !== userId)
                );
            } else {
                setUnblockedUserIds((prev) =>
                    prev.includes(userId) ? prev : [...prev, userId]
                );
            }
        } catch (err) {
            console.error("Błąd zmiany statusu zablokowanego użytkownika:", err);
            setActionError("Wystąpił błąd podczas zmiany statusu zablokowanego użytkownika.");
        } finally {
            setChangingBlockedUserIds((prev) =>
                prev.filter((id) => id !== userId)
            );
        }
    };

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

    return (
        <div className="page-shell">
            <div className="page-container">
                <h1 className="page-title">
                    Zablokowani użytkownicy
                </h1>

                {loading && (
                    <div className="page-info">
                        Ładowanie zablokowanych użytkowników...
                    </div>
                )}

                {!loading && error && (
                    <div className="page-info page-error">
                        {error}
                    </div>
                )}

                {!loading && !error && users.length === 0 && (
                    <div className="page-info">
                        Nie masz jeszcze zablokowanych użytkowników.
                    </div>
                )}

                {!loading && !error && users.length > 0 && (
                    <>
                        {actionError && (
                            <div className="page-info page-error">
                                {actionError}
                            </div>
                        )}

                        <div className="result-list">
                            {paginatedUsers.map((user) => {
                                const userId = getUserId(user);
                                const profileImage = profileImages[userId];
                                const isChanging = changingBlockedUserIds.includes(userId);
                                const isUnblocked = unblockedUserIds.includes(userId);

                                return (
                                    <div
                                        key={userId || user.username}
                                        className="result-card"
                                    >
                                        <Link
                                            to={getUserProfilePath(user)}
                                            className="result-card-left result-card-link"
                                        >
                                            <div className="result-card-media">
                                                {profileImage ? (
                                                    <img
                                                        src={getImageSrc(profileImage)}
                                                        alt="avatar"
                                                    />
                                                ) : (
                                                    <svg
                                                        viewBox="0 0 24 24"
                                                        width="34"
                                                        height="34"
                                                        fill="none"
                                                        xmlns="http://www.w3.org/2000/svg"
                                                    >
                                                        <path
                                                            d="M12 12c2.761 0 5-2.239 5-5S14.761 2 12 2 7 4.239 7 7s2.239 5 5 5Z"
                                                            fill="currentColor"
                                                        />
                                                        <path
                                                            d="M4 22c0-4.418 3.582-8 8-8s8 3.582 8 8"
                                                            stroke="currentColor"
                                                            strokeWidth="2"
                                                            strokeLinecap="round"
                                                        />
                                                    </svg>
                                                )}
                                            </div>

                                            <div className="result-card-content">
                                                <div className="result-card-title">
                                                    {user.displayName || user.name || "Imię i Nazwisko"}
                                                </div>

                                                <div className="result-card-text">
                                                    {user.username || "pseudonim"}
                                                </div>
                                            </div>
                                        </Link>

                                        <button
                                            type="button"
                                            className="result-card-action"
                                            onClick={() => handleToggleBlock(userId)}
                                            disabled={isChanging}
                                            aria-label={isUnblocked ? "Zablokuj ponownie" : "Odblokuj użytkownika"}
                                            title={isUnblocked ? "Zablokuj ponownie" : "Odblokuj użytkownika"}
                                        >
                                            <BlockIcon isUnblocked={isUnblocked} />
                                        </button>
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