import {useEffect, useState} from "react";
import "../../css/login_register.css";
import SuccessPopup from "../../components/Popup/SuccessPopup";

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

    //prawo jazdy
    const [hasDrivingLicense, setHasDrivingLicense] = useState("");
    const [skipDrivingLicense, setSkipDrivingLicense] = useState(false);

    //lokalizacja
    const [locationSearch, setLocationSearch] = useState("");
    const [allLocations, setAllLocations] = useState([]);
    const [skipLocation, setSkipLocation] = useState(false);

    //styl
    const [travelStyle, setTravelStyle] = useState("");
    const [skipTravelStyle, setSkipTravelStyle] = useState(false);

    //doświadczenie
    const [travelExperience, setTravelExperience] = useState("");
    const [skipTravelExperience, setSkipTravelExperience] = useState(false);

    //zainteresowania
    const [allInterests, setAllInterests] = useState([]);
    const [interestsSearch, setInterestsSearch] = useState("");
    const [selectedInterests, setSelectedInterests] = useState([]);
    const [skipInterests, setSkipInterests] = useState(false);

    //transport
    const [selectedTransport, setSelectedTransport] = useState([]);
    const [skipTransport, setSkipTransport] = useState(false);

    //popup
    const [isPopupOpen, setIsPopupOpen] = useState(false);

    const travelStyleOptions = [
        { value: "spontaniczny", label: "spontaniczny" },
        { value: "troche_zaplanowany", label: "trochę zaplanowany" },
        { value: "szczegolowo_zaplanowany", label: "szczegółowo zaplanowany" },
    ];
    const transportOptions = [
        { value: "samochod", label: "samochód" },
        { value: "samolot", label: "samolot" },
        { value: "pieszo", label: "pieszo" },
        { value: "pociag", label: "pociąg" },
        { value: "statek", label: "statek" },
        { value: "autostop", label: "autostop" },
        { value: "autobus", label: "autobus" },
        { value: "obojetnie", label: "obojętnie" },
    ];

    function toggleTransport(option) {
        setSelectedTransport((prev) =>
            prev.includes(option)
                ? prev.filter((o) => o !== option)
                : [...prev, option]
        );
    }

    useEffect(() => {
        const loadLanguages = async () => {
            try {
                const response = await fetch("http://localhost:5292/api/languages/languages/pl", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    credentials: "include",
                });

                const text = await response.text();

                if (!response.ok) {
                    throw new Error("Błąd HTTP " + response.status);
                }

                const data = JSON.parse(text);

                setAllLanguages(data);
            } catch (e) {
                console.error("Błąd ładowania języków", e);
            }
        };

        loadLanguages();
    }, []);


    const filteredLanguages = allLanguages.filter((lang) =>
        lang.toLowerCase().includes(languageSearch.toLowerCase())
    );

    const visibleLanguages = filteredLanguages.slice(0, 6);

    function toggleLanguage(lang) {
        setSelectedLanguages((prev) =>
            prev.includes(lang)
                ? prev.filter((l) => l !== lang)
                : [...prev, lang]
        );
    }

    useEffect(() => {
        const loadLocations = async () => {
            try {
                // TODO: api
            } catch (e) {
                console.error("Błąd ładowania lokalizacji", e);
            }
        };

        void loadLocations();
    }, []);

    useEffect(() => {
        const loadInterests = async () => {
            try {
                // TODO: api

                const interestOptions = [
                    "sport",
                    "książki",
                    "sztuka",
                    "fotografia",
                    "pisanie",
                    "muzyka",
                    "hodowla pszczół",
                ];
                setAllInterests(interestOptions);
            } catch (e) {
                console.error("Błąd ładowania zainteresowań", e);
            }
        };

        void loadInterests();
    }, []);

    const filteredInterests = allInterests.filter((interest) =>
        interest.toLowerCase().includes(interestsSearch.toLowerCase())
    );

    function toggleInterest(interest) {
        setSelectedInterests((prev) =>
            prev.includes(interest)
                ? prev.filter((i) => i !== interest)
                : [...prev, interest]
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
        setStep(5);
    }

    function handleNextFromSubstances() {
        setStep(5);
    }

    function handleBackToStep4() {
        setStep(4);
    }

    function handleSkipDrivingLicense() {
        setSkipDrivingLicense(true);
        setHasDrivingLicense("");
        setStep(6);
    }

    function handleNextFromDrivingLicense() {
        setStep(6);
    }

    function handleBackToStep5() {
        setStep(5);
    }

    function handleSkipLocation() {
        setSkipLocation(true);
        setLocationSearch("");
        setStep(7);
    }

    function handleNextFromLocation() {
        setStep(7);
    }

    function handleBackToStep6() {
        setStep(6);
    }

    function handleSkipTravelStyle() {
        setSkipTravelStyle(true);
        setTravelStyle("");
        setStep(8)
    }

    function handleNextFromTravelStyle() {
        setStep(8);
    }

    function handleBackToStep7() {
        setStep(7);
    }

    function handleSkipTravelExperience() {
        setSkipTravelExperience(true);
        setTravelExperience("");
        setStep(9)
    }

    function handleNextFromTravelExperience() {
        setStep(9);
    }
    function handleBackToStep8() {
        setStep(8);
    }

    function handleSkipInterests() {
        setSkipInterests(true);
        setSelectedInterests([]);
        setStep(10)
    }

    function handleNextFromInterests() {
        setStep(10);
    }

    function handleBackToStep9() {
        setStep(9);
    }

    function handleSkipTransport() {
        setSkipTransport(true);
        setSelectedTransport([]);
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
            hasDrivingLicense: skipDrivingLicense ? null : hasDrivingLicense || null,
            homeLocation: skipLocation ? null : (locationSearch || null),
            travelStyle: skipTravelStyle ? null : (travelStyle || null),
            travelExperience: skipTravelExperience ? null : (travelExperience || null),
            interests: skipInterests
                ? null
                : selectedInterests.length > 0
                    ? selectedInterests
                    : null,
            preferredTransport: skipTransport
                ? null
                : selectedTransport.length > 0
                    ? selectedTransport
                    : null,
        };

        console.log("Dane do wysłania do backendu:", payload);
        setIsPopupOpen(true);
    }
    function handlePopupClose() {
        setIsPopupOpen(false);
        window.location.href = "/login";
    }

    return (
        <div className="auth">
            <div className="auth__page">
                <div className="auth__card">
                    <h1 className="auth__title auth__title--center">
                    Podaj więcej informacji o sobie<br />
                    aby ulepszyć wyszukiwania
                </h1>

                {step === 1 && (
                    <>
                        <div className="form-field">
                            <label className="form-label" htmlFor="languageSearch">
                                Jakimi językami się posługujesz?
                            </label>
                            <input
                                id="languageSearch"
                                type="text"
                                placeholder="Search"
                                className="form-input"
                                value={languageSearch}
                                onChange={(e) =>
                                    setLanguageSearch(e.target.value)
                                }
                            />
                        </div>

                        <div className="pill-group">
                            {visibleLanguages.map((lang) => (
                                <button
                                    key={lang}
                                    type="button"
                                    className={
                                        "pill pill--selectable" +
                                        (selectedLanguages.includes(lang)
                                            ? " pill--selected"
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

                        <div className="form-footer">
                            <button
                                type="button"
                                className="btn btn--primary"
                                onClick={handleNextFromLanguages}
                                disabled={selectedLanguages.length === 0}
                            >
                                Dalej &gt;
                            </button>

                            <button
                                type="button"
                                className="btn btn--secondary"
                                onClick={handleSkipLanguages}
                            >
                                Pomiń
                            </button>
                        </div>
                    </>
                )}

                {step === 2 && (
                    <>
                        <div className="form-field">
                            <label className="form-label" htmlFor="gender">Jakiej jesteś płci?</label>
                            <select
                                id="gender"
                                value={gender}
                                className="form-input"
                                onChange={(e) => setGender(e.target.value)}
                            >
                                <option value="">Select</option>
                                <option value="female">Kobieta</option>
                                <option value="male">Mężczyzna</option>
                                <option value="nonbinary">Inna</option>
                                <option value="no-answer">Wolę nie podawać</option>
                            </select>
                        </div>

                        <div className="form-field">
                            <label className="form-label" htmlFor="pronouns">
                                Jakie masz zaimki?
                            </label>
                            <select
                                id="pronouns"
                                value={pronouns}
                                className="form-input"
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

                        <div className="form-footer">
                            <div className="button-row">
                                <button
                                    type="button"
                                    className="btn btn--secondary"
                                    onClick={handleBackToStep1}
                                >
                                    &lt; Wróć
                                </button>

                                <button
                                    type="button"
                                    className="btn btn--primary"
                                    onClick={handleNextFromGender}
                                    disabled={gender === "" && pronouns === ""}
                                >
                                    Dalej &gt;
                                </button>
                            </div>

                            <button
                                type="button"
                                className="btn btn--secondary"
                                onClick={handleSkipGenderPronouns}
                            >
                                Pomiń
                            </button>
                        </div>
                    </>
                )}
                {step === 3 && (
                    <>
                        <div className="form-field">
                            <label className="form-label" htmlFor="personalityType">
                                Jaki masz typ osobowości?
                            </label>
                            <select
                                id="personalityType"
                                value={personalityType}
                                className="form-input"
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

                        <div className="form-footer">
                            <div className="button-row">
                                <button
                                    type="button"
                                    className="btn btn--secondary"
                                    onClick={handleBackToStep2}
                                >
                                    &lt; Wróć
                                </button>

                                <button
                                    type="button"
                                    className="btn btn--primary"
                                    onClick={handleNextFromPersonality}
                                    disabled={personalityType === ""}
                                >
                                    Dalej &gt;
                                </button>
                            </div>

                            <button
                                type="button"
                                className="btn btn--secondary"
                                onClick={handleSkipPersonality}
                            >
                                Pomiń
                            </button>
                        </div>
                    </>
                )}
                {step === 4 && (
                    <>
                        <div className="form-field">
                            <label className="form-label" htmlFor="alcoholAttitude">
                                Jaki masz stosunek do alkoholu?
                            </label>
                            <select
                                id="alcoholAttitude"
                                value={alcoholAttitude}
                                className="form-input"
                                onChange={(e) => setAlcoholAttitude(e.target.value)}
                            >
                                <option value="">Select</option>
                                <option value="drinking">Piję</option>
                                <option value="occasionally">Piję okazjonalnie</option>
                                <option value="none-tolerating">Nie piję i nie przeszkadza mi</option>
                                <option value="no-tolerating">Nie toleruję</option>
                            </select>
                        </div>

                        <div className="form-field">
                            <label className="form-label" htmlFor="smokingAttitude">
                                Jaki masz stosunek do papierosów?
                            </label>
                            <select
                                id="smokingAttitude"
                                value={smokingAttitude}
                                className="form-input"
                                onChange={(e) => setSmokingAttitude(e.target.value)}
                            >
                                <option value="">Select</option>
                                <option value="smoking">Palę</option>
                                <option value="occasionally">Palę okazjonalnie</option>
                                <option value="none-tolerating">Nie palę i nie przeszkadza mi</option>
                                <option value="no-tolerating">Nie toleruję</option>
                            </select>
                        </div>

                        <div className="form-footer">
                            <div className="button-row">
                                <button
                                    type="button"
                                    className="btn btn--secondary"
                                    onClick={handleBackToStep3}
                                >
                                    &lt; Wróć
                                </button>

                                <button
                                    type="button"
                                    className="btn btn--primary"
                                    onClick={handleNextFromSubstances}
                                    disabled={alcoholAttitude === "" && smokingAttitude === ""}
                                >
                                    Dalej &gt;
                                </button>
                            </div>

                            <button
                                type="button"
                                className="btn btn--secondary"
                                onClick={handleSkipSubstances}
                            >
                                Pomiń
                            </button>
                        </div>
                    </>
                )}
                {step === 5 && (
                    <>
                        <div className="form-field">
                            <label className="form-label" htmlFor="hasDrivingLicense">
                                Czy posiadasz prawo jazdy?
                            </label>
                            <select
                                id="hasDrivingLicense"
                                value={hasDrivingLicense}
                                className="form-input"
                                onChange={(e) => setHasDrivingLicense(e.target.value)}
                            >
                                <option value="">Select</option>
                                <option value="national">Posiadam międzynarodowe</option>
                                <option value="no">Nie posiadam</option>
                                <option value="different">Inne</option>
                            </select>
                        </div>

                        <div className="form-footer">
                            <div className="button-row">
                                <button
                                    type="button"
                                    className="btn btn--secondary"
                                    onClick={handleBackToStep4}
                                >
                                    &lt; Wróć
                                </button>

                                <button
                                    type="button"
                                    className="btn btn--primary"
                                    onClick={handleNextFromDrivingLicense}
                                    disabled={hasDrivingLicense === ""}
                                >
                                    Dalej &gt;
                                </button>
                            </div>

                            <button
                                type="button"
                                className="btn btn--secondary"
                                onClick={handleSkipDrivingLicense}
                            >
                                Pomiń
                            </button>
                        </div>
                    </>
                )}
                {step === 6 && (
                    <>
                        <div className="form-field">
                            <label className="form-label" htmlFor="locationSearch">
                                Skąd jesteś?
                            </label>
                            <input
                                id="locationSearch"
                                type="text"
                                placeholder="Search"
                                className="form-input"
                                value={locationSearch}
                                onChange={(e) => setLocationSearch(e.target.value)}
                            />
                        </div>

                        <div className="form-footer">
                            <div className="button-row">
                                <button
                                    type="button"
                                    className="btn btn--secondary"
                                    onClick={handleBackToStep5}
                                >
                                    &lt; Wróć
                                </button>

                                <button
                                    type="button"
                                    className="btn btn--primary"
                                    onClick={handleNextFromLocation}
                                    disabled={locationSearch.trim() === ""}
                                >
                                    Dalej &gt;
                                </button>
                            </div>

                            <button
                                type="button"
                                className="btn btn--secondary"
                                onClick={handleSkipLocation}
                            >
                                Pomiń
                            </button>
                        </div>
                    </>
                )}
                {step === 7 && (
                    <>
                        <div className="form-field">
                            <label className="form-label" htmlFor="travelStyle">
                                Jaki styl podróżowania preferujesz?
                            </label>

                            <div className="pill-group">
                                {travelStyleOptions.map((option) => (
                                    <button
                                        key={option.value}
                                        type="button"
                                        className={
                                            "pill pill--selectable" +
                                            (travelStyle === option.value
                                                ? " pill--selected"
                                                : "")
                                        }
                                        onClick={() => setTravelStyle(option.value)}
                                    >
                                        {option.label}
                                    </button>
                                ))}
                            </div>
                        </div>

                        <div className="form-footer">
                            <div className="button-row">
                                <button
                                    type="button"
                                    className="btn btn--secondary"
                                    onClick={handleBackToStep6}
                                >
                                    &lt; Wróć
                                </button>

                                <button
                                    type="button"
                                    className="btn btn--primary"
                                    onClick={handleNextFromTravelStyle}
                                    disabled={travelStyle === ""}
                                >
                                    Dalej &gt;
                                </button>
                            </div>

                            <button
                                type="button"
                                className="btn btn--secondary"
                                onClick={handleSkipTravelStyle}
                            >
                                Pomiń
                            </button>
                        </div>
                    </>
                )}
                {step === 8 && (
                    <>
                        <div className="form-field">
                            <label className="form-label" htmlFor="travelExperience">
                                Jakie masz doświadczenie w podróżach?
                            </label>
                            <select
                                id="travelExperience"
                                value={travelExperience}
                                className="form-input"
                                onChange={(e) => setTravelExperience(e.target.value)}
                            >
                                <option value="">Select</option>
                                <option value="beginner">Początkujący</option>
                                <option value="experienced">Doświadczony podróżnik</option>
                            </select>
                        </div>

                        <div className="form-footer">
                            <div className="button-row">
                                <button
                                    type="button"
                                    className="btn btn--secondary"
                                    onClick={handleBackToStep7}
                                >
                                    &lt; Wróć
                                </button>

                                <button
                                    type="button"
                                    className="btn btn--primary"
                                    onClick={handleNextFromTravelExperience}
                                    disabled={travelExperience === ""}
                                >
                                    Dalej &gt;
                                </button>
                            </div>

                            <button
                                type="button"
                                className="btn btn--secondary"
                                onClick={handleSkipTravelExperience}
                            >
                                Pomiń
                            </button>
                        </div>
                    </>
                )}
                {step === 9 && (
                    <>
                        <div className="form-field">
                            <label className="form-label" htmlFor="interestsSearch">
                                Jakie są twoje zainteresowania?
                            </label>
                            <input
                                id="interestsSearch"
                                type="text"
                                placeholder="Search"
                                className="form-input"
                                value={interestsSearch}
                                onChange={(e) => setInterestsSearch(e.target.value)}
                            />
                        </div>

                        <div className="pill-group">
                            {filteredInterests.map((interest) => (
                                <button
                                    key={interest}
                                    type="button"
                                    className={
                                        "pill pill--selectable" +
                                        (selectedInterests.includes(interest)
                                            ? " pill--selected"
                                            : "")
                                    }
                                    onClick={() => toggleInterest(interest)}
                                >
                                    {interest}
                                </button>
                            ))}

                            {filteredInterests.length === 0 && (
                                <p className="text-center">
                                    Brak wyników dla podanego wyszukiwania.
                                </p>
                            )}
                        </div>

                        <div className="form-footer">
                            <div className="button-row">
                                <button
                                    type="button"
                                    className="btn btn--secondary"
                                    onClick={handleBackToStep8}
                                >
                                    &lt; Wróć
                                </button>

                                <button
                                    type="button"
                                    className="btn btn--primary"
                                    onClick={handleNextFromInterests}
                                    disabled={selectedInterests.length === 0}
                                >
                                    Dalej &gt;
                                </button>
                            </div>

                            <button
                                type="button"
                                className="btn btn--secondary"
                                onClick={handleSkipInterests}
                            >
                                Pomiń
                            </button>
                        </div>
                    </>
                )}
                {step === 10 && (
                    <>
                        <div className="form-field">
                            <label className="form-label" htmlFor="preferredTransport">
                                Jakie środki transportu preferujesz?
                            </label>

                            <div className="pill-group">
                                {transportOptions.map((option) => (
                                    <button
                                        key={option.value}
                                        type="button"
                                        className={
                                            "pill pill--selectable" +
                                            (selectedTransport.includes(option.value)
                                                ? " pill--selected"
                                                : "")
                                        }
                                        onClick={() => toggleTransport(option.value)}
                                    >
                                        {option.label}
                                    </button>
                                ))}
                            </div>
                        </div>

                        <div className="form-footer">
                            <div className="button-row">
                                <button
                                    type="button"
                                    className="btn btn--secondary"
                                    onClick={handleBackToStep9}
                                >
                                    &lt; Wróć
                                </button>

                                <button
                                    type="button"
                                    className="btn btn--primary"
                                    onClick={handleFinish}
                                    disabled={selectedTransport.length === 0}
                                >
                                    Zakończ
                                </button>
                            </div>

                            <button
                                type="button"
                                className="btn btn--secondary"
                                onClick={handleSkipTransport}
                            >
                                Pomiń
                            </button>
                        </div>
                    </>
                )}

            </div>
        </div>
            <SuccessPopup
                open={isPopupOpen}
                title="Rejestracja zakończona pomyślnie"
                buttonLabel="Okej"
                onClose={handlePopupClose}
            />
        </div>
    );
}
