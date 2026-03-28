import {useEffect, useState} from "react";
import "../../css/login_register.css";
import SuccessPopup from "../../components/Popup/SuccessPopup";
import AsyncSelect from 'react-select/async';
import useAuth from "../../hooks/useAuth";

export default function AdditionalInfo() {
    const { user, loading } = useAuth();

    const [step, setStep] = useState(1);

    //języki
    const [allLanguages, setAllLanguages] = useState([]);
    const [languageSearch, setLanguageSearch] = useState("");
    const [selectedLanguages, setSelectedLanguages] = useState([]);
    const [skipLanguages, setSkipLanguages] = useState(false);

    //płeć, zaimki
    const [gendersOptions, setGendersOptions] = useState([]);
    const [gender, setGender] = useState("");
    const [pronounsOptions, setPronounsOptions] = useState([]);
    const [pronouns, setPronouns] = useState("");
    const [skipGenderPronouns, setSkipGenderPronouns] = useState(false);

    //typ osobowości
    const [personalityOptions, setPersonalityOptions] = useState([]);
    const [personalityType, setPersonalityType] = useState("");
    const [skipPersonality, setSkipPersonality] = useState(false);

    //alkohol, papierosy
    const [alcoholOptions, setAlcoholOptions] = useState([]);
    const [alcoholAttitude, setAlcoholAttitude] = useState("");
    const [smokingOptions, setSmokingOptions] = useState([]);
    const [smokingAttitude, setSmokingAttitude] = useState("");
    const [skipSubstances, setSkipSubstances] = useState(false);

    //prawo jazdy
    const [drivingOptions, setDrivingOptions] = useState([]);
    const [hasDrivingLicense, setHasDrivingLicense] = useState("");
    const [skipDrivingLicense, setSkipDrivingLicense] = useState(false);

    //lokalizacja
    const [selectedLocation, setSelectedLocation] = useState(null);
    const [skipLocation, setSkipLocation] = useState(false);

    //styl
    const [travelStyleOptions, setTravelStyleOptions] = useState([]);
    const [travelStyleSearch, setTravelStyleSearch] = useState("");
    const [selectedTravelStyles, setSelectedTravelStyles] = useState([]);
    const [skipTravelStyle, setSkipTravelStyle] = useState(false);

    //doświadczenie
    const [travelExperienceOptions, setTravelExperienceOptions] = useState([]);
    const [travelExperience, setTravelExperience] = useState("");
    const [skipTravelExperience, setSkipTravelExperience] = useState(false);

    //zainteresowania
    const [allInterests, setAllInterests] = useState([]);
    const [interestsSearch, setInterestsSearch] = useState("");
    const [selectedInterests, setSelectedInterests] = useState([]);
    const [skipInterests, setSkipInterests] = useState(false);

    //transport
    const [transportOptions, setTransportOptions] = useState([]);
    const [transportSearch, setTransportSearch] = useState("");
    const [selectedTransport, setSelectedTransport] = useState([]);
    const [skipTransport, setSkipTransport] = useState(false);

    //popup
    const [isPopupOpen, setIsPopupOpen] = useState(false);

    useEffect(() => {
        const fetchOptions = async () => {
            try {
                const res = await fetch("http://localhost:5292/api/options", {
                    method: "GET",
                    headers: { "Content-Type": "application/json" },
                    credentials: "include"
                });
                if (!res.ok) throw new Error("Nie udało się pobrać opcji");
                const data = await res.json();

                setGendersOptions(data.genders);
                setPronounsOptions(data.pronouns);
                setPersonalityOptions(data.personalities);
                setAlcoholOptions(data.alcohol);
                setSmokingOptions(data.smoking);
                setDrivingOptions(data.driving);
                setTravelExperienceOptions(data.travelExperience);
            } catch (err) {
                console.error("Błąd ładowania opcji:", err);
            }
        };

        fetchOptions();
    }, []);

    useEffect(() => {
        const loadTransportModes = async () => {
            try {
                const res = await fetch("http://localhost:5292/api/transportmodes");

                if (!res.ok) throw new Error("Błąd transportów");

                const data = await res.json();

                setTransportOptions(data);
            } catch (e) {
                console.error("Błąd ładowania transportów", e);
            }
        };

        loadTransportModes();
    }, []);

    const filteredTransport = transportOptions.filter(t =>
        t.name.toLowerCase().includes(transportSearch.toLowerCase())
    );

    function toggleTransport(id) {
        setSelectedTransport(prev =>
            prev.includes(id)
                ? prev.filter(t => t !== id)
                : [...prev, id]
        );
    }

    useEffect(() => {
        const loadTravelStyles = async () => {
            try {
                const res = await fetch("http://localhost:5292/api/travelstyles");

                if (!res.ok) throw new Error("Błąd stylów podróży");

                const data = await res.json();

                setTravelStyleOptions(data);
            } catch (e) {
                console.error("Błąd ładowania stylów podróży", e);
            }
        };

        loadTravelStyles();
    }, []);

    const filteredTravelStyles = travelStyleOptions.filter(t =>
        t.name.toLowerCase().includes(travelStyleSearch.toLowerCase())
    );

    function toggleTravelStyle(id) {
        setSelectedTravelStyles(prev =>
            prev.includes(id)
                ? prev.filter(t => t !== id)
                : [...prev, id]
        );
    }

    const loadLocationOptions = async (inputValue) => {
        if (!inputValue || inputValue.length < 2) return [];

        try {
            const res = await fetch(
                `https://nominatim.openstreetmap.org/search?format=json&q=${inputValue}&addressdetails=1&limit=5`
            );

            const data = await res.json();

            return data.map(item => ({
                label: item.display_name,
                value: {
                    name: item.display_name,
                    lat: item.lat,
                    lon: item.lon
                }
            }));
        } catch (e) {
            console.error("Błąd lokalizacji", e);
            return [];
        }
    };

    useEffect(() => {
        const loadLanguages = async () => {
            try {
                const response = await fetch("http://localhost:5292/api/languages", {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    credentials: "include",
                });

                if (!response.ok) {
                    throw new Error("Błąd HTTP " + response.status);
                }

                const data = await response.json();

                setAllLanguages(data);
            } catch (e) {
                console.error("Błąd ładowania języków", e);
            }
        };

        loadLanguages();
    }, []);


    const filteredLanguages = allLanguages.filter((lang) =>
        lang.name.toLowerCase().includes(languageSearch.toLowerCase())
    );

    const visibleLanguages = filteredLanguages.slice(0, 6);

    function toggleLanguage(id) {
        setSelectedLanguages((prev) =>
            prev.includes(id)
                ? prev.filter((l) => l !== id)
                : [...prev, id]
        );
    }

    useEffect(() => {
        const loadInterests = async () => {
            try {
                const res = await fetch("http://localhost:5292/api/interests");

                if (!res.ok) {
                    throw new Error("Nie udało się pobrać zainteresowań");
                }

                const data = await res.json();

                setAllInterests(data);
            } catch (e) {
                console.error("Błąd ładowania zainteresowań", e);
            }
        };

        loadInterests();
    }, []);

    const filteredInterests = allInterests.filter((interest) =>
        interest.name.toLowerCase().includes(interestsSearch.toLowerCase())
    );

    const visibleInterests = filteredInterests.slice(0, 6);

    function toggleInterest(id) {
        setSelectedInterests((prev) =>
            prev.includes(id)
                ? prev.filter((i) => i !== id)
                : [...prev, id]
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
        setSelectedLocation(null);
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
        setSelectedTravelStyles([]);
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

    async function handleFinish() {
        const userId = localStorage.getItem("userId");

        if (!userId) {
            console.error("Brak ID użytkownika");
            return;
        }

        const payload = {
            genderId: skipGenderPronouns ? null : (gender || null),
            pronounsId: skipGenderPronouns ? null : (pronouns || null),

            location: skipLocation
                ? null
                : selectedLocation
                    ? selectedLocation.value.name
                    : null,

            personalityTypeId: skipPersonality ? null : (personalityType || null),

            alcoholPreferenceId: skipSubstances ? null : (alcoholAttitude || null),
            smokingPreferenceId: skipSubstances ? null : (smokingAttitude || null),

            drivingLicenseTypeId: skipDrivingLicense ? null : (hasDrivingLicense || null),

            travelExperienceId: skipTravelExperience ? null : (travelExperience || null),

            languageIds: skipLanguages ? [] : selectedLanguages,

            interestIds: skipInterests ? [] : selectedInterests,

            travelStyleIds: skipTravelStyle ? [] : selectedTravelStyles,

            transportModeIds: skipTransport ? [] : selectedTransport
        };

        try {
            const res = await fetch(`http://localhost:5292/api/users/${userId}/additional`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                credentials: "include",
                body: JSON.stringify(payload)
            });

            if (!res.ok) {
                throw new Error("Nie udało się zapisać dodatkowych informacji");
            }

            setIsPopupOpen(true);
        } catch (e) {
            console.error("Błąd zapisu:", e);
        }
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
                                    key={lang.id}
                                    type="button"
                                    className={
                                        "pill pill--selectable" +
                                        (selectedLanguages.includes(lang.id)
                                            ? " pill--selected"
                                            : "")
                                    }
                                    onClick={() => toggleLanguage(lang.id)}
                                >
                                    {lang.name}
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
                                {gendersOptions.map(g => (
                                    <option key={g.id} value={g.id}>{g.name}</option>
                                ))}
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
                                {pronounsOptions.map(p => (
                                    <option key={p.id} value={p.id}>{p.name}</option>
                                ))}
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
                                {personalityOptions.map(p => (
                                    <option key={p.id} value={p.id}>{p.name}</option>
                                ))}
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
                                {alcoholOptions.map(a => (
                                    <option key={a.id} value={a.id}>{a.name}</option>
                                ))}
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
                                {smokingOptions.map(s => (
                                    <option key={s.id} value={s.id}>{s.name}</option>
                                ))}
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
                                {drivingOptions.map(d => (
                                    <option key={d.id} value={d.id}>{d.name}</option>
                                ))}
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
                            <AsyncSelect
                                cacheOptions
                                loadOptions={loadLocationOptions}
                                defaultOptions
                                value={selectedLocation}
                                onChange={setSelectedLocation}
                                placeholder="Wpisz miasto..."
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
                                    disabled={!selectedLocation}
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
                                {filteredTravelStyles.map((option) => (
                                    <button
                                        key={option.id}
                                        type="button"
                                        className={
                                            "pill pill--selectable" +
                                            (selectedTravelStyles.includes(option.id)
                                                ? " pill--selected"
                                                : "")
                                        }
                                        onClick={() => toggleTravelStyle(option.id)}
                                    >
                                        {option.name}
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
                                    disabled={selectedTravelStyles.length === 0}
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
                                {travelExperienceOptions.map(t => (
                                    <option key={t.id} value={t.id}>{t.name}</option>
                                ))}
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
                            {visibleInterests.map((interest) => (
                                <button
                                    key={interest.id}
                                    type="button"
                                    className={
                                        "pill pill--selectable" +
                                        (selectedInterests.includes(interest.id)
                                            ? " pill--selected"
                                            : "")
                                    }
                                    onClick={() => toggleInterest(interest.id)}
                                >
                                    {interest.name}
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
                                {filteredTransport.map((option) => (
                                    <button
                                        key={option.id}
                                        type="button"
                                        className={
                                            "pill pill--selectable" +
                                            (selectedTransport.includes(option.id)
                                                ? " pill--selected"
                                                : "")
                                        }
                                        onClick={() => toggleTransport(option.id)}
                                    >
                                        {option.name}
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
                                    disabled={!skipTransport && selectedTransport.length === 0}
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
