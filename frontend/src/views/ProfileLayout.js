import { Outlet } from "react-router-dom";
import { useLayoutEffect, useRef, useState, useEffect } from "react";
import ProfileHeader from "./ProfileHeader";
import ProfileTabs from "./ProfileTabs";

function calculateAge(birthDate) {
    if (!birthDate) return null;

    const birth = new Date(birthDate);
    const today = new Date();

    let age = today.getFullYear() - birth.getFullYear();
    const monthDiff = today.getMonth() - birth.getMonth();

    if (
        monthDiff < 0 ||
        (monthDiff === 0 && today.getDate() < birth.getDate())
    ) {
        age--;
    }

    return age;
}

export default function ProfileLayout() {
    const [profile, setProfile] = useState(null);
    const [loading, setLoading] = useState(true);

    const topRef = useRef(null);

    useEffect(() => {
        async function loadProfile() {
            try {
                const res = await fetch("http://localhost:5292/api/users/me", {
                    credentials: "include",
                });

                if (!res.ok) {
                    setProfile(null);
                    return;
                }

                const data = await res.json();

                setProfile({
                    name: data.displayName,
                    username: data.username,
                    email: data.email,

                    birthDate: data.birthDate,
                    age: calculateAge(data.birthDate),

                    gender: data.gender,
                    pronouns: data.pronouns,
                    personality: data.personality,
                    location: data.location,
                    description: data.bio,
                    languages: data.languages ?? [],
                    additional: data.additional ?? {},
                });

            } catch (e) {
                console.error("Błąd pobierania profilu", e);
            } finally {
                setLoading(false);
            }
        }

        loadProfile();
    }, []);

    useLayoutEffect(() => {
        if (!topRef.current) return;

        const el = topRef.current;

        const update = () => {
            const h = el.getBoundingClientRect().height;
            document.documentElement.style.setProperty(
                "--profile-sticky-h",
                `${h}px`
            );
        };

        update();
        requestAnimationFrame(update);

        const ro = new ResizeObserver(update);
        ro.observe(el);

        window.addEventListener("resize", update);

        return () => {
            ro.disconnect();
            window.removeEventListener("resize", update);
        };
    }, []);

    if (loading || !profile) return null;

    const isMe = true;

    return (
        <div className="profile-page">
            <div className="profile-topSticky" ref={topRef}>
                <ProfileHeader
                    name={profile.name}
                    age={profile.age}
                    username={profile.username}
                    isMe={isMe}
                    rating={4.8}    //TODO
                />
                <ProfileTabs isMe={isMe} />
            </div>

            <div className="profile-outlet">
                <Outlet context={{ profile, isMe }} />
            </div>
        </div>
    );
}
