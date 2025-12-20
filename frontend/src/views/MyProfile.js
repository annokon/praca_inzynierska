import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import useAuth from "../hooks/useAuth";
import Profile from "./Profile";

export default function MyProfile() {
    const { user, loading } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        if (!loading && !user) {
            navigate("/login", { replace: true });
        }
    }, [loading, user, navigate]);

    if (loading) return null;
    if (!user) return null;

    const profile = {
        name: "Anna Nowak",
        age: 28,
        username: "a_nowak",

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


    return <Profile profile={profile} isMe={true} />;
}