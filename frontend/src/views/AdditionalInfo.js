import {useEffect, useState} from "react";
import "../login/login.css";

export default function AdditionalInfo() {
    const [step, setStep] = useState(1);

    //języki
    const [allLanguages, setAllLanguages] = useState([]);
    const [languageSearch, setLanguageSearch] = useState("");
    const [selectedLanguages, setSelectedLanguages] = useState([]);
    const [skipLanguages, setSkipLanguages] = useState(false);

    //płeć, zaimki
    const [gender, setGender] = useState("");
    const [pronouns, setPronouns] = useState("");
    const [skipGenderPronouns, setSkipGenderPronouns] = useState(false);

    //typ osobowości
    const [personalityType, setPersonalityType] = useState("");
    const [skipPersonality, setSkipPersonality] = useState(false);

    //alkohol, papierosy
    const [alcoholAttitude, setAlcoholAttitude] = useState("");
    const [smokingAttitude, setSmokingAttitude] = useState("");
    const [skipSubstances, setSkipSubstances] = useState(false);


    useEffect(() => {
        const loadLanguages = async () => {
            try {
                // TODO: api

                const languageOptions = [
                    "Polski",
                    "Angielski",
                    "Włoski",
                    "Hiszpański",
                    "Niemiecki",
                    "Niderlandzki",
                    "Francuski",
                ];
                setAllLanguages(languageOptions);
            } catch (e) {
                console.error("Błąd ładowania języków", e);
            }
        };

        void loadLanguages();
    }, []);


    const filteredLanguages = allLanguages.filter((lang) =>
        lang.toLowerCase().includes(languageSearch.toLowerCase())
    );

    function toggleLanguage(lang) {
        setSelectedLanguages((prev) =>
            prev.includes(lang)
                ? prev.filter((l) => l !== lang)
                : [...prev, lang]
        );
    }

    function handleNextFromLanguages() {
        setStep(2);
    }

    function handleBackToStep1() {
        setStep(1);
    }

    function handleSkipLanguages() {
        setSkipLanguages(true);
        setSelectedLanguages([]);
        setStep(2);
    }

    function handleNextFromGender() {
        setStep(3);
    }

    function handleSkipGenderPronouns() {
        setSkipGenderPronouns(true);
        setGender("");
        setPronouns("");
        setStep(3);
    }

    function handleBackToStep2() {
        setStep(2);
    }

    function handleSkipPersonality() {
        setSkipPersonality(true);
        setPersonalityType("");
        setStep(4);
    }

    function handleNextFromPersonality() {
        setStep(4);
    }

    function handleBackToStep3() {
        setStep(3);
    }

    function handleSkipSubstances() {
        setSkipSubstances(true);
        setAlcoholAttitude("");
        setSmokingAttitude("");
        handleFinish();
    }


    function handleFinish() {
        const payload = {
            languages: skipLanguages
                ? null
                : selectedLanguages.length > 0
                    ? selectedLanguages
                    : null,
            gender: skipGenderPronouns ? null : gender || null,
            pronouns: skipGenderPronouns ? null : pronouns || null,
            personalityType: skipPersonality ? null : personalityType || null,
            alcoholAttitude: skipSubstances ? null : alcoholAttitude || null,
            smokingAttitude: skipSubstances ? null : smokingAttitude || null,
        };

        console.log("Dane do wysłania do backendu:", payload);
        window.location.href = "/";
    }

    return (
        <div className="login-page">
            <div className="card">
                <h1 className="text-center">
                    Podaj więcej informacji o sobie<br />
                    aby ulepszyć wyszukiwania
                </h1>

                {step === 1 && (
                    <>
                        <div className="field">
                            <label htmlFor="languageSearch">
                                Jakimi językami się posługujesz?
                            </label>
                            <input
                                id="languageSearch"
                                type="text"
                                placeholder="Search"
                                value={languageSearch}
                                onChange={(e) =>
                                    setLanguageSearch(e.target.value)
                                }
                            />
                        </div>

                        <div className="languages-list">
                            {filteredLanguages.map((lang) => (
                                <button
                                    key={lang}
                                    type="button"
                                    className={
                                        "pill-button" +
                                        (selectedLanguages.includes(lang)
                                            ? " pill-button--selected"
                                            : "")
                                    }
                                    onClick={() => toggleLanguage(lang)}
                                >
                                    {lang}
                                </button>
                            ))}
                            {filteredLanguages.length === 0 && (
                                <p className="text-center">
                                    Brak wyników dla podanego wyszukiwania.
                                </p>
                            )}
                        </div>

                        <div className="footer">
                            <button
                                type="button"
                                className="button"
                                onClick={handleNextFromLanguages}
                            >
                                Dalej &gt;
                            </button>

                            <button
                                type="button"
                                className="link-button"
                                onClick={handleSkipLanguages}
                            >
                                Pomiń
                            </button>
                        </div>
                    </>
                )}

                {step === 2 && (
                    <>
                        <div className="field">
                            <label htmlFor="gender">Jakiej jesteś płci?</label>
                            <select
                                id="gender"
                                value={gender}
                                onChange={(e) => setGender(e.target.value)}
                            >
                                <option value="">Select</option>
                                <option value="female">Kobieta</option>
                                <option value="male">Mężczyzna</option>
                                <option value="nonbinary">Inna</option>
                                <option value="no-answer">Wolę nie podawać</option>
                            </select>
                        </div>

                        <div className="field">
                            <label htmlFor="pronouns">
                                Jakie masz zaimki?
                            </label>
                            <select
                                id="pronouns"
                                value={pronouns}
                                onChange={(e) => setPronouns(e.target.value)}
                            >
                                <option value="">Select</option>
                                <option value="she-her">ona / jej</option>
                                <option value="he-him">on / jego</option>
                                <option value="they-them">oni / ich</option>
                                <option value="custom">Inne</option>
                                <option value="no-answer">Wolę nie podawać</option>
                            </select>
                        </div>

                        <div className="footer">
                            <div className="button-row">
                                <button
                                    type="button"
                                    className="button secondary"
                                    onClick={handleBackToStep1}
                                >
                                    &lt; Wróć
                                </button>

                                <button
                                    type="button"
                                    className="button"
                                    onClick={handleNextFromGender}
                                >
                                    Dalej &gt;
                                </button>
                            </div>

                            <button
                                type="button"
                                className="link-button"
                                onClick={handleSkipGenderPronouns}
                            >
                                Pomiń
                            </button>
                        </div>
                    </>
                )}
                {step === 3 && (
                    <>
                        <div className="field">
                            <label htmlFor="personalityType">
                                Jaki masz typ osobowości?
                            </label>
                            <select
                                id="personalityType"
                                value={personalityType}
                                onChange={(e) =>
                                    setPersonalityType(e.target.value)
                                }
                            >
                                <option value="">Select</option>
                                <option value="introvert">Introwertyk</option>
                                <option value="extrovert">Ekstrawertyk</option>
                                <option value="ambivert">Ambiwertyk</option>
                            </select>
                        </div>

                        <div className="footer">
                            <div className="button-row">
                                <button
                                    type="button"
                                    className="button secondary"
                                    onClick={handleBackToStep2}
                                >
                                    &lt; Wróć
                                </button>

                                <button
                                    type="button"
                                    className="button"
                                    onClick={handleNextFromPersonality}
                                >
                                    Dalej &gt;
                                </button>
                            </div>

                            <button
                                type="button"
                                className="link-button"
                                onClick={handleSkipPersonality}
                            >
                                Pomiń
                            </button>
                        </div>
                    </>
                )}
                {step === 4 && (
                    <>
                        <div className="field">
                            <label htmlFor="alcoholAttitude">
                                Jaki masz stosunek do alkoholu?
                            </label>
                            <select
                                id="alcoholAttitude"
                                value={alcoholAttitude}
                                onChange={(e) => setAlcoholAttitude(e.target.value)}
                            >
                                <option value="">Select</option>
                                <option value="drinking">Piję</option>
                                <option value="occasionally">Piję okazjonalnie</option>
                                <option value="none-tolerating">Nie piję i nie przeszkadza mi</option>
                                <option value="no-tolerating">Nie toleruję</option>
                            </select>
                        </div>

                        <div className="field">
                            <label htmlFor="smokingAttitude">
                                Jaki masz stosunek do papierosów?
                            </label>
                            <select
                                id="smokingAttitude"
                                value={smokingAttitude}
                                onChange={(e) => setSmokingAttitude(e.target.value)}
                            >
                                <option value="">Select</option>
                                <option value="smoking">Palę</option>
                                <option value="occasionally">Palę okazjonalnie</option>
                                <option value="none-tolerating">Nie palę i nie przeszkadza mi</option>
                                <option value="no-tolerating">Nie toleruję</option>
                            </select>
                        </div>

                        <div className="footer">
                            <div className="button-row">
                                <button
                                    type="button"
                                    className="button secondary"
                                    onClick={handleBackToStep3}
                                >
                                    &lt; Wróć
                                </button>

                                <button
                                    type="button"
                                    className="button"
                                    onClick={handleFinish}
                                >
                                    Dalej &gt;
                                </button>
                            </div>

                            <button
                                type="button"
                                className="link-button"
                                onClick={handleSkipSubstances}
                            >
                                Pomiń
                            </button>
                        </div>
                    </>
                )}
            </div>
        </div>
    );
}
