import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "../../../css/settings.css";

export default function SettingsAppearance() {
    const navigate = useNavigate();

    const [profileFile, setProfileFile] = useState(null);
    const [bannerFile, setBannerFile] = useState(null);

    const [profilePreview, setProfilePreview] = useState("");
    const [bannerPreview, setBannerPreview] = useState("");

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

    const handleSubmit = async (e) => {
        e.preventDefault();

        const formData = new FormData();

        if (profileFile) formData.append("profileImage", profileFile);
        if (bannerFile) formData.append("bannerImage", bannerFile);

        try {
            const res = await fetch("http://localhost:5292/api/users/me/images", {
                method: "POST",
                body: formData,
                credentials: "include",
            });

            if (!res.ok) throw new Error("Upload failed");

            const data = await res.json();
            console.log(data);

            navigate(-1);
        } catch (err) {
            console.error(err);
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
                            <label className="settings-btn settings-btn--ghost settings-input-trigger">
                                Wybierz nowe zdjęcie
                                <input
                                    type="file"
                                    accept="image/*"
                                    onChange={(e) => setProfileFile(e.target.files?.[0] || null)}
                                />
                            </label>

                            <div className="settings-preview settings-preview--square">
                                {profilePreview ? (
                                    <img src={profilePreview} alt="Podgląd zdjęcia profilowego" />
                                ) : (
                                    <span className="settings-preview-placeholder">Podgląd</span>
                                )}
                            </div>
                        </div>
                    </section>

                    <section className="settings-section">
                        <h2 className="settings-section-title">Zmień banner w tle</h2>

                        <div className="settings-stack">
                            <label className="settings-btn settings-btn--ghost settings-input-trigger">
                                Wybierz nowe zdjęcie
                                <input
                                    type="file"
                                    accept="image/*"
                                    onChange={(e) => setBannerFile(e.target.files?.[0] || null)}
                                />
                            </label>

                            <div className="settings-preview settings-preview--wide">
                                {bannerPreview ? (
                                    <img src={bannerPreview} alt="Podgląd bannera" />
                                ) : (
                                    <span className="settings-preview-placeholder">Podgląd</span>
                                )}
                            </div>
                        </div>
                    </section>

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