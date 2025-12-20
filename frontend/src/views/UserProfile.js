import React from "react";
import { useParams } from "react-router-dom";
import Profile from "./Profile";

export default function UserProfile() {
    const { id } = useParams();

    const profile = {
        name: "Michał Kowalski",
        age: 30,
        username: `user_${id}`,

        gender: "mężczyzna",
        pronouns: "on/jego",
        personality: "introwertyk",
        location: "Kraków, Polska",

        description: "Lubię spokojne podróże, naturę i dobre jedzenie.",

        languages: ["polski", "angielski"],

        additional: {
            interests: ["Góry", "Kuchnia", "Fotografia"],
            transport: ["pociąg", "samochód"],
            travelStyle: ["zaplanowany"],
            experience: ["średniozaawansowany"],
            drivingLicense: ["posiadam"],
            alcohol: ["nie piję"],
            smoking: ["nie palę"],
        },
    };

    return <Profile profile={profile} isMe={false} />;
}
