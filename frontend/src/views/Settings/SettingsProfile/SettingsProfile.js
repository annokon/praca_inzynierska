import React from "react";
import { useNavigate } from "react-router-dom";
import Settings from "../Settings";

export default function SettingsProfile() {
    const navigate = useNavigate();

    const groups = [
        {
            key: "main",
            items: [
                { key: "edit", label: "Edytuj dane", onClick: () => navigate("/settings-change-data") },
                { key: "look", label: "Zmień wygląd profilu", onClick: () => navigate("/settings-appearance") },
                { key: "lang", label: "Zmień język", onClick: () => navigate("/settings-language") },
                { key: "currency", label: "Zmień walutę", onClick: () => navigate("/settings-currency") },
            ],
        },
        {
            key: "blocked",
            items: [
                { key: "blockedUsers", label: "Zablokowani użytkownicy", onClick: () => navigate("/settings-blocked") },
            ],
        },
        {
            key: "account",
            items: [
                { key: "logout", label: "Wyloguj", onClick: () => navigate("/logout") },
                {
                    key: "delete",
                    label: "Usuń profil",
                    onClick: () => navigate("/settings-delete-account"),
                    tone: "danger",
                    right: "danger",
                },
            ],
        },
    ];

    return (
        <Settings
            title="Ustawienia"
            subtitle="Ustawienia profilu"
            groups={groups}
            onBack={() => navigate(-1)}
        />
    );
}
