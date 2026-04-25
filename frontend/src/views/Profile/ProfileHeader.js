import React from "react";
import "../../css/profile.css";
import {
    MessageCircleMore,
    HeartPlus,
    Star,
} from "lucide-react";

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
                            {<Star size={24} strokeWidth={2} className="icon-muted" color={"#F59E0B"} />}
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
                        <div className="ph-stat-divider"></div>
                    </div>

                    <div className="ph-actions">
                        {!isMe ? (
                            <>
                                <button className="btn-primary" onClick={onMessageClick}>
                                    {<MessageCircleMore size={18} strokeWidth={2} className="icon-muted" />}
                                    Wyślij wiadomość
                                </button>
                                <button className="btn-secondary" onClick={onFollowClick}>
                                    {<HeartPlus size={18} strokeWidth={2} className="icon-muted" />}
                                    Polub
                                </button>
                            </>
                        ) : (
                            <button className="btn-primary">Edytuj profil</button>
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