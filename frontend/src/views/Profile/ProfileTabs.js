import { NavLink } from "react-router-dom";
import "../../css/profile.css";
import {
    User,
    TentTree,
    CircleStar,
    Trophy,
    SlidersHorizontal,
    Luggage,
} from "lucide-react";
import React from "react";

function Pill({ children, colorClass = "" }) {
    return <span className={`pill ${colorClass}`}>{children}</span>;
}

function InfoLine({ icon, title, value, colorClass }) {
    if (!value) return null;
    return (
        <div className="info-line">
            <div className="info-line__left">
                {icon}
                <span className="info-line__title">{title}</span>
            </div>
            <div className="info-line__value">
                <Pill colorClass={colorClass}>{value}</Pill>
            </div>
        </div>
    );
}

export default function ProfileTabs({ isMe }) {
    return (
        <div className="profile-tabs-bar">
            <div className="tabs">
                <NavLink to="about-user" className={({ isActive }) => `tab ${isActive ? "is-active" : ""}`}>
                    {<User size={18} strokeWidth={2} className="icon-muted" />}
                    O użytkowniku
                </NavLink>
                <NavLink to="trips" className={({ isActive }) => `tab ${isActive ? "is-active" : ""}`}>
                    {<TentTree size={18} strokeWidth={2} className="icon-muted" />}
                    Podróże
                </NavLink>
                <NavLink to="reviews" className={({ isActive }) => `tab ${isActive ? "is-active" : ""}`}>
                    {<CircleStar size={18} strokeWidth={2} className="icon-muted" />}
                    Opinie
                </NavLink>
                <NavLink to="achievements" className={({ isActive }) => `tab ${isActive ? "is-active" : ""}`}>
                    {<Trophy size={18} strokeWidth={2} className="icon-muted" />}
                    Osiągnięcia
                </NavLink>
                {isMe && (
                    <NavLink to="fav-filters" className={({ isActive }) => `tab ${isActive ? "is-active" : ""}`}>
                        {<SlidersHorizontal size={18} strokeWidth={2} className="icon-muted" />}
                        Filtry wyszukiwania
                    </NavLink>
                )}
            </div>
        </div>
    );
}