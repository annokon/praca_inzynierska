import { Outlet, useParams } from "react-router-dom";
import { useLayoutEffect, useRef, useState, useEffect } from "react";
import ProfileHeader from "./ProfileHeader";
import ProfileTabs from "./ProfileTabs";
import "../../css/profile.css";

function calculateAge(birthDate) {
    if (!birthDate) return null;
    const birth = new Date(birthDate);
    const today = new Date();
    let age = today.getFullYear() - birth.getFullYear();
    const monthDiff = today.getMonth() - birth.getMonth();
    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birth.getDate())) {
        age--;
    }
    return age;
}

export default function ProfileLayout() {
    const { username } = useParams();
    const [profile, setProfile] = useState(null);
    const [images, setImages] = useState({ profile: null, banner: null });
    const [loading, setLoading] = useState(true);
    const [isMe, setIsMe] = useState(false);

    const topRef = useRef(null);

    useEffect(() => {
        async function loadProfile() {
            setLoading(true);
            try {
                const url = `http://localhost:5292/api/users/by-username/${username}/profile`;

                const res = await fetch(url, { credentials: "include" });

                if (!res.ok) {
                    setProfile(null);
                    setLoading(false);
                    return;
                }

                const data = await res.json();

                setIsMe(data.isMe);

                setProfile({
                    id: data.id,
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
                });

                const imgRes = await fetch(`http://localhost:5292/api/users/${data.id}/images`, {
                    credentials: "include",
                });

                if (imgRes.ok) {
                    const imgData = await imgRes.json();
                    setImages({ profile: imgData.profile, banner: imgData.banner });
                }
            } catch (e) {
                console.error("Błąd pobierania profilu", e);
            } finally {
                setLoading(false);
            }
        }

        loadProfile();
    }, [username]);

    useLayoutEffect(() => {
        if (!topRef.current) return;
        const el = topRef.current;
        const update = () => {
            const h = el.getBoundingClientRect().height;
            document.documentElement.style.setProperty("--profile-sticky-h", `${h}px`);
        };
        update();
        const ro = new ResizeObserver(update);
        ro.observe(el);
        return () => ro.disconnect();
    }, [profile]);

    if (loading) return <div className="profile-loading-screen">Ładowanie profilu...</div>;
    if (!profile) return <div className="profile-error-screen">Nie znaleziono użytkownika.</div>;

    return (
        <div className="profile-page">
            <div className="profile-topSticky" ref={topRef}>
                <ProfileHeader
                    name={profile.name}
                    age={profile.age}
                    username={profile.username}
                    isMe={isMe}
                    rating={4.8}    //TODO
                    trips={12}  //TODO
                    profileImage={images.profile}
                    bannerImage={images.banner}
                />
                <ProfileTabs isMe={isMe} />
            </div>

            <div className="profile-outlet">
                <Outlet context={{ profile, isMe }} />
            </div>
        </div>
    );
}