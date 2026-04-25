import React, { useContext, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../../context/AuthContext";
import "../../../css/settings.css";

const API_BASE_URL = "http://localhost:5292";

function getImageUrl(imagePath) {
    if (!imagePath) return "";

    if (imagePath.startsWith("http") || imagePath.startsWith("blob:")) {
        return imagePath;
    }

    return `${API_BASE_URL}/${imagePath.replace(/^\/+/, "")}`;
}

export default function SettingsAppearance() {
    const navigate = useNavigate();
    const { user, setUser } = useContext(AuthContext);

    const [status, setStatus] = useState("");

    const [profileFile, setProfileFile] = useState(null);
    const [bannerFile, setBannerFile] = useState(null);

    const [profilePreview, setProfilePreview] = useState("");
    const [bannerPreview, setBannerPreview] = useState("");

    const [removeProfileImage, setRemoveProfileImage] = useState(false);
    const [removeBannerImage, setRemoveBannerImage] = useState(false);

    useEffect(() => {
        if (!profileFile) {
            setProfilePreview("");
            return;
        }

        const objectUrl = URL.createObjectURL(profileFile);
        setProfilePreview(objectUrl);

        return () => URL.revokeObjectURL(objectUrl);
    }, [profileFile]);

    useEffect(() => {
        if (!bannerFile) {
            setBannerPreview("");
            return;
        }

        const objectUrl = URL.createObjectURL(bannerFile);
        setBannerPreview(objectUrl);

        return () => URL.revokeObjectURL(objectUrl);
    }, [bannerFile]);

    const profileImageToShow = useMemo(() => {
        if (removeProfileImage) return "";
        return profilePreview || getImageUrl(user?.profileImage);
    }, [removeProfileImage, profilePreview, user?.profileImage]);

    const bannerImageToShow = useMemo(() => {
        if (removeBannerImage) return "";
        return bannerPreview || getImageUrl(user?.bannerImage);
    }, [removeBannerImage, bannerPreview, user?.bannerImage]);

    const handleProfileChange = (e) => {
        const file = e.target.files?.[0] || null;

        setProfileFile(file);
        setRemoveProfileImage(false);
        setStatus("");
    };

    const handleBannerChange = (e) => {
        const file = e.target.files?.[0] || null;

        setBannerFile(file);
        setRemoveBannerImage(false);
        setStatus("");
    };

    const handleRemoveProfileImage = () => {
        setProfileFile(null);
        setProfilePreview("");
        setRemoveProfileImage(true);
        setStatus("Zdjęcie profilowe zostanie usunięte po kliknięciu Zatwierdź.");
    };

    const handleRemoveBannerImage = () => {
        setBannerFile(null);
        setBannerPreview("");
        setRemoveBannerImage(true);
        setStatus("Banner zostanie usunięty po kliknięciu Zatwierdź.");
    };

    const refreshUserAfterSave = async () => {
        try {
            const res = await fetch(`${API_BASE_URL}/api/users/me`, {
                method: "GET",
                credentials: "include",
            });

            if (!res.ok) return;

            const updatedUser = await res.json();

            if (typeof setUser === "function") {
                setUser(updatedUser);
            }
        } catch (err) {
            console.error("Nie udało się odświeżyć danych użytkownika:", err);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setStatus("");

        const formData = new FormData();

        if (profileFile) {
            formData.append("profileImage", profileFile);
        } else if (removeProfileImage) {
            formData.append("profileImage", "-1");
        }

        if (bannerFile) {
            formData.append("bannerImage", bannerFile);
        } else if (removeBannerImage) {
            formData.append("bannerImage", "-1");
        }

        if (
            !profileFile &&
            !bannerFile &&
            !removeProfileImage &&
            !removeBannerImage
        ) {
            setStatus("Nie wybrano żadnych zmian.");
            return;
        }

        try {
            setStatus("Zapisywanie zmian...");

            const res = await fetch(`${API_BASE_URL}/api/users/me/images`, {
                method: "POST",
                body: formData,
                credentials: "include",
            });

            const data = await res.json().catch(() => ({}));

            if (!res.ok) {
                console.error("Błąd zapisu zdjęć:", res.status, data);
                setStatus(data.message || "Nie udało się zapisać zmian.");
                return;
            }

            if (typeof setUser === "function") {
                setUser((prev) => ({
                    ...prev,
                    profileImage: removeProfileImage ? null : prev?.profileImage,
                    bannerImage: removeBannerImage ? null : prev?.bannerImage,
                }));
            }

            await refreshUserAfterSave();

            navigate(-1);
        } catch (err) {
            console.error(err);
            setStatus("Wystąpił błąd połączenia z serwerem.");
        }
    };

    return (
        <div className="settings-page">
            <div className="settings-panel settings-panel--form">
                <div className="settings-header">
                    <h1 className="settings-title">Ustawienia</h1>
                    <p className="settings-subtitle">Zmień wygląd profilu</p>
                </div>

                <form className="settings-form" onSubmit={handleSubmit}>
                    <section className="settings-section">
                        <h2 className="settings-section-title">Zmień profilowe</h2>

                        <div className="settings-row settings-row--split">
                            <div className="settings-action-column">
                                <label className="settings-btn settings-btn--ghost settings-input-trigger">
                                    Wybierz nowe zdjęcie
                                    <input
                                        type="file"
                                        accept="image/*"
                                        onChange={handleProfileChange}
                                    />
                                </label>

                                <button
                                    type="button"
                                    className="settings-btn settings-btn--ghost"
                                    onClick={handleRemoveProfileImage}
                                >
                                    Usuń zdjęcie
                                </button>
                            </div>

                            <div className="settings-preview settings-preview--square">
                                {profileImageToShow ? (
                                    <img
                                        src={profileImageToShow}
                                        alt="Podgląd zdjęcia profilowego"
                                    />
                                ) : (
                                    <span className="settings-preview-placeholder">
                                        Podgląd
                                    </span>
                                )}
                            </div>
                        </div>
                    </section>

                    <section className="settings-section">
                        <h2 className="settings-section-title">Zmień banner w tle</h2>

                        <div className="settings-stack">
                            <div className="settings-action-row">
                                <label className="settings-btn settings-btn--ghost settings-input-trigger">
                                    Wybierz nowe zdjęcie
                                    <input
                                        type="file"
                                        accept="image/*"
                                        onChange={handleBannerChange}
                                    />
                                </label>

                                <button
                                    type="button"
                                    className="settings-btn settings-btn--ghost"
                                    onClick={handleRemoveBannerImage}
                                >
                                    Usuń zdjęcie
                                </button>
                            </div>

                            <div className="settings-preview settings-preview--wide">
                                {bannerImageToShow ? (
                                    <img
                                        src={bannerImageToShow}
                                        alt="Podgląd bannera"
                                    />
                                ) : (
                                    <span className="settings-preview-placeholder">
                                        Podgląd
                                    </span>
                                )}
                            </div>
                        </div>
                    </section>

                    {status && (
                        <p className="settings-status">
                            {status}
                        </p>
                    )}

                    <div className="settings-actions">
                        <button
                            type="button"
                            className="settings-btn settings-btn--ghost"
                            onClick={() => navigate(-1)}
                        >
                            Anuluj
                        </button>

                        <button type="submit" className="settings-btn settings-btn--primary">
                            Zatwierdź
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}