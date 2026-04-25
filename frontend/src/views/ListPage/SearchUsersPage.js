import React, { useEffect, useMemo, useState } from "react";
import { Link, useSearchParams } from "react-router-dom";
import "../../css/list_page.css";

const API_BASE_URL = "http://localhost:5292";

const USERS_PER_PAGE = 10;
const MAX_PAGES = 10;

function getProfileImageFromResponse(data) {
    if (!data) return "";

    if (typeof data === "string") {
        return data;
    }

    if (data.profile) return data.profile;
    if (data.profileImage) return data.profileImage;
    if (data.profileImageUrl) return data.profileImageUrl;

    if (data.image) return data.image;
    if (data.imageUrl) return data.imageUrl;
    if (data.path) return data.path;
    if (data.filePath) return data.filePath;

    if (Array.isArray(data.images)) {
        const profileImage =
            data.images.find((image) => image.isProfile) ||
            data.images.find((image) => image.isMain) ||
            data.images[0];

        return getProfileImageFromResponse(profileImage);
    }

    if (Array.isArray(data.userImages)) {
        const profileImage =
            data.userImages.find((image) => image.isProfile) ||
            data.userImages.find((image) => image.isMain) ||
            data.userImages[0];

        return getProfileImageFromResponse(profileImage);
    }

    return "";
}

function getImageSrc(imagePath) {
    if (!imagePath) return "";

    if (imagePath.startsWith("http://") || imagePath.startsWith("https://")) {
        return encodeURI(imagePath);
    }

    if (imagePath.startsWith("/")) {
        return encodeURI(`${API_BASE_URL}${imagePath}`);
    }

    return encodeURI(`${API_BASE_URL}/${imagePath}`);
}

export default function SearchUsersPage() {
    const [searchParams] = useSearchParams();
    const query = (searchParams.get("q") || "").trim();

    const [users, setUsers] = useState([]);
    const [profileImages, setProfileImages] = useState({});
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [currentPage, setCurrentPage] = useState(1);

    useEffect(() => {
        setCurrentPage(1);
        setProfileImages({});
    }, [query]);

    useEffect(() => {
        const loadUsers = async () => {
            if (!query) {
                setUsers([]);
                setLoading(false);
                setError("");
                return;
            }

            setLoading(true);
            setError("");

            try {
                const res = await fetch(
                    `${API_BASE_URL}/api/users/search?q=${encodeURIComponent(query)}&limit=${USERS_PER_PAGE * MAX_PAGES}`,
                    {
                        method: "GET",
                        credentials: "include"
                    }
                );

                if (!res.ok) {
                    throw new Error("Błąd odpowiedzi z serwera");
                }

                const data = await res.json();

                setUsers(Array.isArray(data) ? data : []);
            } catch (err) {
                console.error("Błąd pobierania wyników wyszukiwania:", err);
                setUsers([]);
                setError("Wystąpił błąd podczas pobierania wyników wyszukiwania.");
            } finally {
                setLoading(false);
            }
        };

        loadUsers();
    }, [query]);

    const visibleUsers = useMemo(() => {
        return users.slice(0, USERS_PER_PAGE * MAX_PAGES);
    }, [users]);

    const totalPages = Math.max(
        1,
        Math.min(MAX_PAGES, Math.ceil(visibleUsers.length / USERS_PER_PAGE))
    );

    const paginatedUsers = useMemo(() => {
        const startIndex = (currentPage - 1) * USERS_PER_PAGE;
        const endIndex = startIndex + USERS_PER_PAGE;

        return visibleUsers.slice(startIndex, endIndex);
    }, [visibleUsers, currentPage]);

    useEffect(() => {
        const loadProfileImages = async () => {
            if (paginatedUsers.length === 0) return;

            const usersToLoad = paginatedUsers.filter((user) => user.id != null);

            if (usersToLoad.length === 0) return;

            const results = await Promise.all(
                usersToLoad.map(async (user) => {
                    try {
                        const res = await fetch(
                            `${API_BASE_URL}/api/users/${user.id}/images`,
                            {
                                method: "GET",
                                credentials: "include"
                            }
                        );

                        if (!res.ok) {
                            return [user.id, ""];
                        }

                        const data = await res.json();

                        console.log(`Zdjęcia użytkownika ${user.id}:`, data);

                        const profileImage = getProfileImageFromResponse(data);

                        return [user.id, profileImage || ""];
                    } catch (err) {
                        console.error(`Błąd pobierania zdjęcia użytkownika ${user.id}:`, err);
                        return [user.id, ""];
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
                    Wyniki wyszukiwania {query}
                </h1>

                {loading && (
                    <div className="page-info">
                        Ładowanie wyników...
                    </div>
                )}

                {!loading && error && (
                    <div className="page-info page-error">
                        {error}
                    </div>
                )}

                {!loading && !error && users.length === 0 && (
                    <div className="page-info">
                        Brak użytkowników dla frazy: <strong>{query}</strong>
                    </div>
                )}

                {!loading && !error && users.length > 0 && (
                    <>
                        <div className="result-list">
                            {paginatedUsers.map((user) => {
                                const profileImage = profileImages[user.id];

                                return (
                                    <Link
                                        key={user.id || user.username}
                                        to={`/profile/${user.username}/about-user`}
                                        className="result-card"
                                    >
                                        <div className="result-card-left">
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
                                                    {user.username}
                                                </div>
                                            </div>
                                        </div>

                                        <div className="result-card-action">
                                            &gt;
                                        </div>
                                    </Link>
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