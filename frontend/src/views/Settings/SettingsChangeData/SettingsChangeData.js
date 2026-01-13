import React from "react";
import { useNavigate } from "react-router-dom";
import Settings from "../Settings";

export default function SettingsChangeData() {
    const navigate = useNavigate();

    const items = [
        { key: "username", label: "Edytuj nazwę użytkownika", onClick: () => navigate("/settings-edit-username") },
        { key: "displayName", label: "Edytuj wyświetlaną nazwę", onClick: () => navigate("/settings-edit-display-name") },
        { key: "birthDate", label: "Edytuj datę urodzenia", onClick: () => navigate("/settings-edit-birthdate") },
        { key: "languages", label: "Edytuj języki", onClick: () => navigate("/settings-edit-languages") },
        { key: "gender", label: "Edytuj płeć", onClick: () => navigate("/settings-edit-gender") },

        { key: "sp1", type: "spacer" },

        { key: "additional", label: "Edytuj dodatkowe\ninformacje", onClick: () => navigate("/settings-info") },
    ].filter((it) => it.key !== "__spacer_1");

    return (
        <Settings
            title="Ustawienia"
            subtitle="Edytuj dane"
            items={[
                { key: "username", label: "Edytuj nazwę użytkownika", onClick: () => navigate("/settings-edit-username") },
                { key: "displayName", label: "Edytuj wyświetlaną nazwę", onClick: () => navigate("/settings-edit-display-name") },
                { key: "birthDate", label: "Edytuj datę urodzenia", onClick: () => navigate("/settings-edit-birthdate") },
                { key: "languages", label: "Edytuj języki", onClick: () => navigate("/settings-edit-languages") },
                { key: "gender", label: "Edytuj płeć", onClick: () => navigate("/settings-edit-gender") },

                { key: "additional", label: "Edytuj dodatkowe\ninformacje", onClick: () => navigate("/settings-info") },
            ]}
            onBack={() => navigate(-1)}
        />
    );
}
