import React from "react";
import "../../css/profile.css";

export default function ProfileHeader({
                                          name,
                                          age,
                                          username,
                                          isMe,
                                          profileImage,
                                          bannerImage,
                                          rating,
                                          trips,
                                          onMessageClick,
                                          onFollowClick,
                                          onOptionsClick
                                      }) {
    return (
        <header className="ph-wrap">
            <div
                className="ph-banner"
                style={{
                    backgroundImage: bannerImage
                        ? `url(http://localhost:5292/${bannerImage})`
                        : "url('https://via.placeholder.com/1200x300?text=Banner')"
                }}
            />
            <div className="ph-content-wrapper">
                <div className="ph-inner">

                    <div className="ph-user-info">
                        <div className="ph-avatar-container">
                            <div className="ph-avatar">
                                {profileImage ? (
                                    <img src={`http://localhost:5292/${profileImage}`} alt="avatar" />
                                ) : (
                                    <svg viewBox="0 0 24 24" width="48" height="48" fill="#cbd5e1" xmlns="http://www.w3.org/2000/svg">
                                        <circle cx="12" cy="8" r="4" fill="currentColor"/>
                                        <path d="M4 22c0-4.418 3.582-8 8-8s8 3.582 8 8" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
                                    </svg>
                                )}
                            </div>
                        </div>

                        <div className="ph-meta">
                            <h1 className="ph-name">
                                {name} <span className="ph-age">{age !== null ? `${age} lat` : ""}</span>
                            </h1>
                            <div className="ph-username">@{username}</div>
                        </div>
                    </div>

                    <div className="ph-stats">
                        <div className="ph-stat-item">
                            <svg viewBox="0 0 24 24" width="24" height="24" fill="none" stroke="#F59E0B" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                                <polygon points="12 2 15.09 8.26 22 9.27 17 14.14 18.18 21.02 12 17.77 5.82 21.02 7 14.14 2 9.27 8.91 8.26 12 2"></polygon>
                            </svg>
                            <div className="ph-stat-text">
                                <strong>{rating}</strong>
                                <span>Ocena</span>
                            </div>
                        </div>
                        <div className="ph-stat-divider"></div>
                        <div className="ph-stat-item">
                            <svg viewBox="0 0 24 24" width="24" height="24" fill="none" stroke="#3B82F6" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                                <rect x="2" y="7" width="20" height="14" rx="2" ry="2"></rect>
                                <path d="M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16"></path>
                            </svg>
                            <div className="ph-stat-text">
                                <strong>{trips}</strong>
                                <span>Podróże</span>
                            </div>
                        </div>
                    </div>

                    <div className="ph-actions">
                        {!isMe ? (
                            <>
                                <button className="btn-primary" onClick={onMessageClick}>
                                    <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="2">
                                        <path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2v10z"></path>
                                    </svg>
                                    Wyślij wiadomość
                                </button>
                                <button className="btn-secondary" onClick={onFollowClick}>
                                    <svg viewBox="0 0 24 24" width="18" height="18" fill="none" stroke="currentColor" strokeWidth="2">
                                        <path d="M16 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path>
                                        <circle cx="8.5" cy="7" r="4"></circle>
                                        <line x1="20" y1="8" x2="20" y2="14"></line>
                                        <line x1="23" y1="11" x2="17" y2="11"></line>
                                    </svg>
                                    Obserwuj
                                </button>
                            </>
                        ) : (
                            <button className="btn-secondary">Edytuj profil</button>
                        )}
                        <button className="btn-icon" onClick={onOptionsClick}>
                            <svg viewBox="0 0 24 24" width="20" height="20" fill="currentColor">
                                <circle cx="12" cy="5" r="2"/>
                                <circle cx="12" cy="12" r="2"/>
                                <circle cx="12" cy="19" r="2"/>
                            </svg>
                        </button>
                    </div>

                </div>
            </div>
        </header>
    );
}