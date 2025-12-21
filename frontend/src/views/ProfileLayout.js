import { Outlet, useParams } from "react-router-dom";
import useAuth from "../hooks/useAuth";
import ProfileHeader from "./ProfileHeader";
import ProfileTabs from "./ProfileTabs";

export default function ProfileLayout() {
    const { username } = useParams();
    const { user, loading } = useAuth();

    const isMe = !!user && user.username === username;

    const profile = {
        name: "Anna Nowak",
        age: 28,
        username,
        rating: 4.8,
        gender: "kobieta",
        pronouns: "on/jej",
        personality: "ekstrawertyk",
        location: "Warszawa, Polska",
        description: "Opis użytkownika",
        languages: ["polski", "angielski", "hiszpański"],
        additional: {
            interests: ["Zainteresowanie 1", "Zainteresowanie 2", "Zainteresowanie 3"],
            transport: ["samolot", "samochód"],
            travelStyle: ["spontaniczny"],
            experience: ["doświadczony podróżnik"],
            drivingLicense: ["posiadam międzynarodowe"],
            alcohol: ["piję okazjonalnie"],
            smoking: ["nie palę i nie przeszkadza mi"],
        },
    };

    if (loading) return null;

    return (
        <div className="profile-page">
            <ProfileHeader
                name={profile.name}
                age={profile.age}
                username={profile.username}
                isMe={isMe}
                rating={profile.rating}
            />

            <ProfileTabs isMe={isMe} />

            <Outlet context={{ profile, isMe }} />
        </div>
    );
}
