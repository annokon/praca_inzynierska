import React, { useEffect, useMemo, useState } from "react";
import { Link, useSearchParams } from "react-router-dom";
import "../../css/list_page.css";

const USERS_PER_PAGE = 10;

export default function SearchUsersPage() {
    const [searchParams] = useSearchParams();
    const query = (searchParams.get("q") || "").trim();

    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [currentPage, setCurrentPage] = useState(1);

    useEffect(() => {
        setCurrentPage(1);
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
                    `http://localhost:5292/api/users/search?q=${encodeURIComponent(query)}&limit=100`,
                    {
                        method: "GET",
                        credentials: "include"
                    }
                );

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

    const totalPages = Math.max(1, Math.ceil(users.length / USERS_PER_PAGE));

    const paginatedUsers = useMemo(() => {
        const startIndex = (currentPage - 1) * USERS_PER_PAGE;
        const endIndex = startIndex + USERS_PER_PAGE;
        return users.slice(startIndex, endIndex);
    }, [users, currentPage]);

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
                            {paginatedUsers.map((user) => (
                                <Link
                                    key={user.id || user.username}
                                    to={`/profile/${user.username}/about-user`}
                                    className="result-card"
                                >
                                    <div className="result-card-left">
                                        <div className="result-card-media">
                                            IMG
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
                            ))}
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