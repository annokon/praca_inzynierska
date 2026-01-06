import React from "react";
import "../css/profile_header.css";

const IconButton = ({ title, children, onClick, className = "ph-iconBtn" }) => (
    <button
        type="button"
        className={className}
        aria-label={title}
        title={title}
        onClick={onClick}
    >
        {children}
    </button>
);

export default function ProfileHeader({
                                          name,
                                          age,
                                          username,
                                          isMe,

                                          liked = false,
                                          onLikeClick,

                                          rating,
                                          onGalleryClick,
                                          onRatingClick,
                                          onTrophyClick,

                                          onReportClick,
                                          onMailClick,
                                          onBlockClick,

                                          onPlaceholder1Click,
                                          onPlaceholder2Click,
                                      }) {

    return (
        <header className="ph-wrap">
            <div className="ph-inner">
                <div className="ph-left">
                    <div className="ph-avatar" aria-hidden="true">
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
                    </div>

                    <div className="ph-meta">
                        <div className="ph-nameRow">
                            <h1 className="ph-name">
                                {name}
                                {age !== null && <> , {age} lat</>}
                            </h1>
                            {!isMe && (
                                <IconButton title="Polub profil" onClick={onLikeClick}>
                                    <svg viewBox="0 0 24 24" width="20" height="20" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <path
                                            d="M12 21s-7-4.35-9.5-8.5C.9 9.3 2.6 6 6 6c1.8 0 3.2 1 4 2
             .8-1 2.2-2 4-2 3.4 0 5.1 3.3 3.5 6.5C19 16.65 12 21 12 21Z"
                                            stroke="currentColor"
                                            strokeWidth="2"
                                            fill={liked ? "currentColor" : "none"}
                                        />
                                    </svg>
                                </IconButton>
                            )}
                            <IconButton title="Zdjęcia" onClick={onGalleryClick}>
                                <svg
                                    viewBox="0 0 24 24"
                                    width="18"
                                    height="18"
                                    fill="none"
                                    xmlns="http://www.w3.org/2000/svg"
                                >
                                    <path
                                        d="M4 7a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v10a2 2 0 0 1-2 2H6a2 2 0 0 1-2-2V7Z"
                                        stroke="currentColor"
                                        strokeWidth="2"
                                    />
                                    <path
                                        d="M8.5 10.5a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3Z"
                                        fill="currentColor"
                                    />
                                    <path
                                        d="M21 16l-5-5-6 6"
                                        stroke="currentColor"
                                        strokeWidth="2"
                                        strokeLinecap="round"
                                        strokeLinejoin="round"
                                    />
                                </svg>
                            </IconButton>
                        </div>

                        <div className="ph-username">@{username}</div>
                    </div>
                </div>

                <div className="ph-right">
                    {!isMe && (
                        <div className="ph-actionsTop">
                            <IconButton title="Zablokuj" onClick={onBlockClick} className="ph-actionMini">
                                <svg viewBox="0 0 24 24" width="18" height="18" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M6 6l12 12" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                                    <path d="M12 22c5.523 0 10-4.477 10-10S17.523 2 12 2 2 6.477 2 12s4.477 10 10 10Z" stroke="currentColor" strokeWidth="2"/>
                                </svg>
                            </IconButton>

                            <IconButton title="Zgłoś użytkownika" onClick={onReportClick} className="ph-actionMini">
                                <svg viewBox="0 0 24 24" width="18" height="18" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M12 9v4" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                                    <path d="M12 17h.01" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                                    <path d="M10.3 4.2 2.6 18a2 2 0 0 0 1.7 3h15.4a2 2 0 0 0 1.7-3L13.7 4.2a2 2 0 0 0-3.4 0Z" stroke="currentColor" strokeWidth="2" strokeLinejoin="round"/>
                                </svg>
                            </IconButton>

                            <IconButton title="Wiadomość" onClick={onMailClick} className="ph-actionMini">
                                <svg viewBox="0 0 24 24" width="18" height="18" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M4 6h16v12H4V6Z" stroke="currentColor" strokeWidth="2" strokeLinejoin="round"/>
                                    <path d="m4 7 8 6 8-6" stroke="currentColor" strokeWidth="2" strokeLinejoin="round"/>
                                </svg>
                            </IconButton>
                        </div>
                    )}

                    <div className="ph-actionsBottom">
                        <IconButton title="Średnia ocen" onClick={onRatingClick} className="ph-actionBox">
                            <div className="ph-rating">
                                <svg viewBox="0 0 24 24" width="20" height="20" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path
                                        d="M12 17.27 18.18 21l-1.64-7.03L22 9.24l-7.19-.61L12 2 9.19 8.63 2 9.24l5.46 4.73L5.82 21 12 17.27Z"
                                        stroke="currentColor"
                                        strokeWidth="2"
                                        strokeLinejoin="round"
                                    />
                                </svg>
                                <span className="ph-rating__text">{rating}</span>
                            </div>
                        </IconButton>

                        <IconButton title="Osiągnięcia" onClick={onTrophyClick} className="ph-actionBox">
                            <svg viewBox="0 0 24 24" width="22" height="22" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M8 21h8M12 17v4M7 4h10v5a5 5 0 0 1-10 0V4Z" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                                <path d="M7 6H4v2a4 4 0 0 0 4 4M17 6h3v2a4 4 0 0 1-4 4" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                            </svg>
                        </IconButton>

                        <IconButton title="Placeholder 1" onClick={onPlaceholder1Click} className="ph-actionBox">
                            <svg viewBox="0 0 24 24" width="22" height="22" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M6 6l12 12" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                                <path d="M18 6L6 18" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                            </svg>
                        </IconButton>

                        <IconButton title="Placeholder 2" onClick={onPlaceholder2Click} className="ph-actionBox">
                            <svg viewBox="0 0 24 24" width="22" height="22" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M6 6l12 12" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                                <path d="M18 6L6 18" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                            </svg>
                        </IconButton>
                    </div>
                </div>
            </div>
        </header>
    );
}