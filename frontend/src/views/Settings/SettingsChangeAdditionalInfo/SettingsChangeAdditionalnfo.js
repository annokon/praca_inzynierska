import React from "react";
import { useNavigate } from "react-router-dom";
import Settings from "../Settings";

export default function SettingsChangeAdditionalnfo() {
    const navigate = useNavigate();

    const items = [
        { key: "nick", label: "Edytuj zaimki", onClick: () => navigate("/settings/pronouns") },
        { key: "about", label: 'Edytuj "o mnie"', onClick: () => navigate("/settings/about") },
        { key: "city", label: "Edytuj miejsce zamieszkania", onClick: () => navigate("/settings/location") },
        { key: "personality", label: "Edytuj osobowość", onClick: () => navigate("/settings/personality") },
        { key: "alcohol", label: "Edytuj stosunek\ndo alkoholu", onClick: () => navigate("/settings/alcohol") },
        { key: "smoke", label: "Edytuj stosunek do\npapierosów", onClick: () => navigate("/settings/smoking") },
        { key: "license", label: "Edytuj prawo jazdy", onClick: () => navigate("/settings/driving") },
        { key: "travelStyle", label: "Edytuj preferowany styl\npodróżowania", onClick: () => navigate("/settings/travel-style") },
        { key: "interests", label: "Edytuj zainteresowania", onClick: () => navigate("/settings/interests") },
        { key: "transport", label: "Edytuj preferowane\nśrodki transportu", onClick: () => navigate("/settings/transport") },
        { key: "experience", label: "Edytuj poziom\ndoświadczenia w podróżach", onClick: () => navigate("/settings/experience") },
    ];

    return (
        <Settings
            title="Ustawienia"
            subtitle="Edytuj dodatkowe informacje"
            items={items}
            onBack={() => navigate(-1)}
        />
    );
}
