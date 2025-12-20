import React, { useState } from "react";
import "../../css/login_register.css";

export default function ForgotPassword() {
    const [status, setStatus] = useState("");
    const [step, setStep] = useState("email");
    const [email, setEmail] = useState("");

    const handleEmailSubmit = async (e) => {
        e.preventDefault();
        const enteredEmail = e.target.email.value;

            setEmail(enteredEmail);
            setStatus("");
            setStep("code");

    };

    const handleCodeSubmit = async (e) => {
        e.preventDefault();
        const code = e.target.code.value;

        if (code === "123456") {
            setStatus("Kod poprawny. Możesz teraz ustawić nowe hasło.");
        } else {
            setStatus("Kod jest nieprawidłowy lub wygasł.");
        }
    };

    return (
        <div className="auth">
            <div className="auth__page">
                <div className="auth__card">
                    <h1 className="auth__title auth__title--center">Odzyskiwanie hasła</h1>

                    {step === "email" && (
                        <>
                            <p className="auth__subtitle auth__subtitle--center">
                                Podaj email, aby wysłać kod do zmiany hasła.
                            </p>

                            <form id="forgotPasswordForm" className="auth__form" onSubmit={handleEmailSubmit}>
                                <div className="form-field">
                                    <label className="form-label" htmlFor="email">
                                        Email
                                    </label>
                                    <input
                                        id="email"
                                        name="email"
                                        type="email"
                                        className="form-input"
                                        required
                                    />
                                </div>

                                <div className="form-footer">
                                    <button type="submit" className="btn btn--primary">
                                        Wyślij email do odzyskania hasła
                                    </button>

                                    <button
                                        type="button"
                                        className="btn btn--secondary"
                                        onClick={() => (window.location.href = "/register")}
                                    >
                                        Nie masz konta? Stwórz nowe teraz
                                    </button>

                                    <div id="status" className="form-status">
                                        {status}
                                    </div>
                                </div>
                            </form>
                        </>
                    )}

                    {step === "code" && (
                        <>
                            <p className="auth__subtitle auth__subtitle--center">
                                Na adres <strong>{email}</strong> został wysłany mail z kodem do
                                odzyskania hasła.
                            </p>

                            <form id="verifyCodeForm" className="auth__form" onSubmit={handleCodeSubmit}>
                                <div className="form__field">
                                    <label className="form-label" htmlFor="code">
                                        Wprowadź kod
                                    </label>
                                    <input
                                        id="code"
                                        name="code"
                                        type="text"
                                        className="form-input"
                                        required
                                    />
                                </div>

                                <div className="form-footer">
                                    <button type="submit" className="btn btn--primary">
                                        Potwierdź
                                    </button>

                                    <button
                                        type="button"
                                        className="btn btn--secondary"
                                        onClick={() => (window.location.href = "/register")}
                                    >
                                        Nie masz konta? Stwórz nowe teraz
                                    </button>

                                    <div id="status" className="form-status">
                                        {status}
                                    </div>
                                </div>
                            </form>
                        </>
                    )}
                </div>
            </div>
        </div>
    );
}
