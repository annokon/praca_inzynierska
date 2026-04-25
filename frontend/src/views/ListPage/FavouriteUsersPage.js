import React, { useEffect, useMemo, useState } from "react";
import { Link } from "react-router-dom";
import "../../css/list_page.css";
import { getProfileImageFromResponse, getImageSrc } from "../../components/utils/imageHelper";

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

export default function FavouriteUsersPage() {
    const [users, setUsers] = useState([]);
    const [profileImages, setProfileImages] = useState({});
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [actionError, setActionError] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const [changingFavouriteUserIds, setChangingFavouriteUserIds] = useState([]);
    const [removedUserIds, setRemovedUserIds] = useState([]);

    useEffect(() => {
        const loadFavouriteUsers = async () => {
            setLoading(true);
            setError("");
            setActionError("");

            try {
                const res = await fetch("http://localhost:5292/api/favourites", {
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
                setRemovedUserIds([]);
            } catch (err) {
                console.error("Błąd pobierania ulubionych użytkowników:", err);
                setUsers([]);
                setError("Wystąpił błąd podczas pobierania ulubionych użytkowników.");
            } finally {
                setLoading(false);
            }
        };

        loadFavouriteUsers();
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
                            `http://localhost:5292/api/users/${userId}/images`,
                            {
                                method: "GET",
                                credentials: "include"
                            }
                        );

                        if (!res.ok) {
                            return [userId, ""];
                        }

                        const data = await res.json();

                        console.log(`Zdjęcia użytkownika ${userId}:`, data);

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

    const handleToggleFavourite = async (userId) => {
        if (userId == null) return;

        if (changingFavouriteUserIds.includes(userId)) return;

        const isRemoved = removedUserIds.includes(userId);
        const method = isRemoved ? "POST" : "DELETE";

        setActionError("");
        setChangingFavouriteUserIds((prev) => [...prev, userId]);

        try {
            const res = await fetch(`http://localhost:5292/api/favourites/${userId}`, {
                method,
                credentials: "include"
            });

            if (!res.ok) {
                const errorText = await res.text();
                console.error("Odpowiedź backendu:", errorText);
                throw new Error(
                    isRemoved
                        ? "Błąd dodawania użytkownika do ulubionych"
                        : "Błąd usuwania użytkownika z ulubionych"
                );
            }

            if (isRemoved) {
                setRemovedUserIds((prev) =>
                    prev.filter((id) => id !== userId)
                );
            } else {
                setRemovedUserIds((prev) =>
                    prev.includes(userId) ? prev : [...prev, userId]
                );
            }
        } catch (err) {
            console.error("Błąd zmiany statusu ulubionego użytkownika:", err);
            setActionError("Wystąpił błąd podczas zmiany statusu ulubionego użytkownika.");
        } finally {
            setChangingFavouriteUserIds((prev) =>
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
                    Ulubieni użytkownicy
                </h1>

                {loading && (
                    <div className="page-info">
                        Ładowanie ulubionych użytkowników...
                    </div>
                )}

                {!loading && error && (
                    <div className="page-info page-error">
                        {error}
                    </div>
                )}

                {!loading && !error && users.length === 0 && (
                    <div className="page-info">
                        Nie masz jeszcze ulubionych użytkowników.
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
                                const isChanging = changingFavouriteUserIds.includes(userId);
                                const isRemoved = removedUserIds.includes(userId);

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
                                                    {user.username || "seudonim"}
                                                </div>
                                            </div>
                                        </Link>

                                        <button
                                            type="button"
                                            className="result-card-action result-card-heart"
                                            onClick={() => handleToggleFavourite(userId)}
                                            disabled={isChanging}
                                            aria-label={isRemoved ? "Dodaj do ulubionych" : "Usuń z ulubionych"}
                                        >
                                            <svg
                                                viewBox="0 0 24 24"
                                                width="28"
                                                height="28"
                                                xmlns="http://www.w3.org/2000/svg"
                                            >
                                                <path
                                                    d="M12 21s-7.2-4.6-9.6-8.7C.2 8.5 2.4 4 6.6 4c2.2 0 4 1.2 5.4 3 1.4-1.8 3.2-3 5.4-3 4.2 0 6.4 4.5 4.2 8.3C19.2 16.4 12 21 12 21Z"
                                                    fill={isRemoved ? "none" : "#ef4444"}
                                                    stroke="#ef4444"
                                                    strokeWidth="1.8"
                                                    strokeLinejoin="round"
                                                />
                                            </svg>
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