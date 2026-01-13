import React from "react";
import "../../css/settings.css";

export default function Settings({
                                     title = "Ustawienia",
                                     subtitle = "",
                                     items = [],
                                     groups = null,
                                     onBack,
                                     footerLabel = "Wróć",
                                 }) {
    const renderItem = (it) => {
        const isDanger = it.tone === "danger";
        const right = it.right === "danger" ? (
            <span className="settings-item__danger" aria-hidden="true">⊘</span>
        ) : (
            <span className="settings-item__chevron" aria-hidden="true">&gt;</span>
        );

        return (
            <button
                key={it.key}
                type="button"
                className={`settings-item ${isDanger ? "settings-item--danger" : ""}`}
                onClick={it.onClick}
                disabled={it.disabled}
                aria-label={it.ariaLabel || it.label}
            >
                <span className="settings-item__label">{it.label}</span>
                {right}
            </button>
        );
    };

    const content = groups ? (
        <div className="settings-groups">
            {groups.map((g, idx) => (
                <React.Fragment key={g.key || idx}>
                    <nav className="settings-list" aria-label={g.ariaLabel || g.title || subtitle || "Lista ustawień"}>
                        {g.items.map(renderItem)}
                    </nav>
                    {idx < groups.length - 1 ? <div className="settings-divider" aria-hidden="true" /> : null}
                </React.Fragment>
            ))}
        </div>
    ) : (
        <nav className="settings-list" aria-label={subtitle || "Lista ustawień"}>
            {items.map((it) => {
                if (it.type === "spacer") {
                    return <div key={it.key} className="settings-spacer" aria-hidden="true" />;
                }

                return (
                    <button
                        key={it.key}
                        type="button"
                        className="settings-item"
                        onClick={it.onClick}
                        disabled={it.disabled}
                        aria-label={it.ariaLabel || it.label}
                    >
                        <span className="settings-item__label">{it.label}</span>
                        <span className="settings-item__chevron" aria-hidden="true">&gt;</span>
                    </button>
                );
            })}
        </nav>
    );

    return (
        <main className="settings-page">
            <section className="settings-panel settings-panel--narrow" aria-label={title}>
                <header className="settings-header">
                    <h1 className="settings-title">{title}</h1>
                    {subtitle ? <p className="settings-subtitle">{subtitle}</p> : null}
                </header>

                {content}

                <footer className="settings-footer">
                    <button
                        type="button"
                        className="settings-btn settings-btn--ghost"
                        onClick={onBack}
                    >
                        {footerLabel}
                    </button>
                </footer>
            </section>
        </main>
    );
}
