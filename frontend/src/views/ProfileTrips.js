import React, { useState } from "react";
import "../css/profile_trips.css";
import {useOutletContext} from "react-router-dom";
import useAuth from "../hooks/useAuth";
import ConfirmPopup from "../components/Popup/ConfirmPopup";
import ProfileTripsAddParticipant from "./ProfileTripsAddParticipant";
import ReviewPopup from "../components/Popup/ReviewPopup";
import SuccessPopup from "../components/Popup/SuccessPopup";


const sampleTrips = [
    {
        id: "t1",
        ownerUserId: 2,
        title: "Londyn wrzesień 2025",
        status: "Zarchiwizowana",
        destination: "Londyn, Anglia",
        type: "zwiedzanie",
        startDate: "23 września 2025",
        endDate: "30 września 2025",
        durationLabel: "5–7 dni",
        description: "Opis ogłoszenia podróży",
        budgetLabel: "9 000 zł",
        participants: [
            { id: "2", name: "Anna Nowak", isCreator: true },
            { id: "p2", name: "Jan Nowak", isCreator: false },
            { id: "p3", name: "Maria Nowak", isCreator: false },
        ],
        photos: Array.from({ length: 11 }, (_, i) => ({ id: `ph-${i + 1}` })),
    },
    {
        id: "t2",
        ownerUserId: 2,
        title: "Amsterdam czerwiec 2025",
        status: "Aktywna",
        destination: "Amsterdam, Holandia",
        type: "wypoczynek",
        startDate: "23 czerwca 2025",
        endDate: "30 czerwca 2025",
        durationLabel: "8 dni",
        description: "Opis ogłoszenia podróży",
        budgetLabel: "7 000 zł",
        participants: [
            { id: "2", name: "Anna Nowak", isCreator: true },
            { id: "p5", name: "Kasia Kowalska", isCreator: false },
        ],
        photos: Array.from({ length: 4 }, (_, i) => ({ id: `phA-${i + 1}` })),
    },
    {
        id: "t3",
        ownerUserId: 3,
        title: "Kopenhaga sierpień 2024",
        status: "Zarchiwizowana",
        destination: "Kopenhaga, Dania",
        type: "zwiedzanie",
        startDate: "23 sierpnia 2024",
        endDate: "30 sierpnia 2024",
        durationLabel: "7 dni",
        description: "Opis ogłoszenia podróży",
        budgetLabel: "6 500 zł",
        participants: [
            { id: "3", name: "Anna Nowak", isCreator: true },
            { id: "p7", name: "Paweł Nowak", isCreator: false },
        ],
        photos: [],
    },
];

export default function ProfileTrips() {
    const { user } = useAuth();
    const { profile, isMe } = useOutletContext();
    const [expandedId, setExpandedId] = useState(null);
    const [trips, setTrips] = useState(sampleTrips);

    const [innerTabByTrip, setInnerTabByTrip] = useState({
        t1: "details",
        t2: "details",
        t3: "details",
    });

    const [addModal, setAddModal] = useState({
        open: false,
        tripId: null,
    });

    const [removePopup, setRemovePopup] = useState({
        open: false,
        tripId: null,
        participantId: null,
        participantName: "",
    });

    const [reviewPopup, setReviewPopup] = useState({
        open: false,
        tripId: null,
        targetUserId: null,
        targetName: "",
    });

    const [reviewSuccessOpen, setReviewSuccessOpen] = useState(false);

    //
    const viewerId = user?.idUser ?? user?.id ?? user?.IdUser;

    const isTripParticipant = (trip) =>
        !!viewerId && trip.participants.some((p) => Number(p.id) === Number(viewerId));

    const isTripOwner = (trip) =>
        !!viewerId && Number(trip.ownerUserId) === Number(viewerId);

    //


    const toggleExpanded = (id) => setExpandedId((prev) => (prev === id ? null : id));
    const setInnerTab = (tripId, tab) => setInnerTabByTrip((prev) => ({ ...prev, [tripId]: tab }));

    const handleReviewClick = (tripId, participantId, participantName) => {
        setReviewPopup({
            open: true,
            tripId,
            targetUserId: participantId,
            targetName: participantName,
        });
    };


    const handleAddPhoto = (tripId) => {

    };
    const handleAddParticipant = (tripId) => {
        setAddModal({ open: true, tripId });
    };

    const handleAddUserToTrip = (user) => {
        const tripId = addModal.tripId;

        setTrips((prev) =>
            prev.map((t) => {
                if (t.id !== tripId) return t;

                const exists = t.participants.some((p) => String(p.id) === String(user.id));
                if (exists) return t;

                return {
                    ...t,
                    participants: [
                        ...t.participants,
                        { id: user.id, name: user.fullName, isCreator: false },
                    ],
                };
            })
        );
    };


    const handleRemoveParticipant = (tripId, participantId) => {
        setTrips((prev) =>
            prev.map((t) => {
                if (t.id !== tripId) return t;
                return {
                    ...t,
                    participants: t.participants.filter(
                        (pp) => String(pp.id) !== String(participantId)
                    ),
                };
            })
        );
    };

    const activeTrip = trips.find((t) => t.id === addModal.tripId);
    const existingIds = new Set((activeTrip?.participants ?? []).map((p) => String(p.id)));


    return (
        <div className="trips">
            <div className="trips__list">
                {trips.map((trip) => {
                    const isExpanded = expandedId === trip.id;
                    const innerTab = innerTabByTrip[trip.id] ?? "details";

                    return (
                        <section key={trip.id} className={`card tripCard ${isExpanded ? "tripCard--expanded" : ""}`}>
                            <button
                                type="button"
                                className="tripCard__header"
                                onClick={() => toggleExpanded(trip.id)}
                                aria-expanded={isExpanded}
                            >
                                <div className="tripCard__titleRow">
                  <span className={`chev ${isExpanded ? "chev--down" : ""}`} aria-hidden="true">
                    <IconChevron />
                  </span>
                                    <h3 className="tripCard__title">{trip.title}</h3>
                                </div>

                                <div className="tripCard__actions" onClick={(e) => e.stopPropagation()}>
                                    {isMe && (
                                        <>
                                            <button className="iconBtn" type="button" title="Edytuj" aria-label="Edytuj">
                                                <IconPencil />
                                            </button>
                                            <button className="iconBtn" type="button" title="Usuń" aria-label="Usuń">
                                                <IconTrash />
                                            </button>
                                        </>
                                    )}
                                </div>
                            </button>

                            {/* zwinięte */}
                            {!isExpanded && (
                                <div className="tripCard__summaryGrid">
                                    <InfoItem label="Status" icon={<IconDot />} value={trip.status} pill />
                                    <InfoItem label="Cel podróży" icon={<IconPin />} value={trip.destination} />
                                    <InfoItem label="Data rozpoczęcia" icon={<IconCalendar />} value={trip.startDate} />
                                    <InfoItem label="Rodzaj" icon={<IconTag />} value={trip.type} pill />
                                    <InfoItem label="Data zakończenia" icon={<IconCalendar />} value={trip.endDate} />
                                </div>
                            )}
                            {/* rozwinięte */}
                            {isExpanded && (
                                <div className="tripCard__expanded">
                                    <div className="tripCard__innerTabs">
                                        <button
                                            type="button"
                                            className={`miniTab ${innerTab === "details" ? "miniTab--active" : ""}`}
                                            onClick={() => setInnerTab(trip.id, "details")}
                                        >
                      <span className="miniTab__icon" aria-hidden="true">
                        <IconList />
                      </span>
                                            Szczegóły podróży
                                        </button>

                                        <button
                                            type="button"
                                            className={`miniTab ${innerTab === "photos" ? "miniTab--active" : ""}`}
                                            onClick={() => setInnerTab(trip.id, "photos")}
                                        >
                      <span className="miniTab__icon" aria-hidden="true">
                        <IconImages />
                      </span>
                                            Zdjęcia
                                        </button>
                                    </div>

                                    {innerTab === "details" ? (
                                        <div className="tripCard__details">
                                            <h4 className="section-subtitle">Informacje o podróży</h4>

                                            <div className="info-line">
                                                <div className="info-line__title">Cel podróży</div>
                                                <div className="desc-box">{trip.destination}</div>
                                            </div>

                                            <div className="info-line">
                                                <div className="info-line__title">Opis</div>
                                                <div className="desc-box">{trip.description}</div>
                                            </div>

                                            <h4 className="section-subtitle">Szczegóły podróży</h4>
                                            <div className="pill-wrap">
                                                <span className="pill"><IconCalendar /> Data rozpoczęcia: {trip.startDate}</span>
                                                <span className="pill"><IconCalendar /> Data zakończenia: {trip.endDate}</span>
                                                <span className="pill"><IconClock /> Czas trwania: {trip.durationLabel}</span>
                                                <span className="pill"><IconTag /> Rodzaj: {trip.type}</span>
                                                <span className="pill"><IconWallet /> Budżet: {trip.budgetLabel}</span>
                                            </div>

                                            <div className="participantsHeader">
                                                <h4 className="section-subtitle participantsHeader__title">Uczestnicy</h4>
                                                {isTripOwner(trip) && (
                                                    <button
                                                        type="button"
                                                        className="iconBtn iconBtn--add"
                                                        aria-label="Dodaj uczestnika"
                                                        title="Dodaj uczestnika"
                                                        onClick={() => handleAddParticipant(trip.id)}
                                                    >
                                                        <IconPlus />
                                                    </button>
                                                )}
                                            </div>
                                            <div className="participants">
                                                {trip.participants.map((p) => (
                                                    <div key={p.id} className="participant">
                                                        <div className="ph-trip-avatar" aria-hidden="true">
                                                            <svg
                                                                viewBox="0 0 24 24"
                                                                width="34"
                                                                height="34"
                                                                fill="none"
                                                                xmlns="http://www.w3.org/2000/svg"
                                                            >
                                                                <path
                                                                    d="M12 12c2.761 0 5-2.239 5-5S14.761 2 12 2 7 4.239 7 7s2.239 5 5 5Z"
                                                                    fill="currentColor"
                                                                />
                                                                <path
                                                                    d="M4 22c0-4.418 3.582-8 8-8s8 3.582 8 8"
                                                                    stroke="currentColor"
                                                                    strokeWidth="2"
                                                                    strokeLinecap="round"
                                                                />
                                                            </svg>
                                                        </div>

                                                        <div className="participant__name">{p.name}</div>

                                                        <div className="participant__right">
                                                            {p.isCreator ? (
                                                                <span className="badge" title="Twórca podróży" aria-label="Twórca podróży">
      <IconCrown />
    </span>
                                                            ) : (
                                                                <>
                                                                    {isTripParticipant(trip) && Number(p.id) !== Number(viewerId) && (
                                                                        <button
                                                                            type="button"
                                                                            className="iconBtn iconBtn--star"
                                                                            title="Wystaw opinię"
                                                                            aria-label="Wystaw opinię"
                                                                            onClick={() => handleReviewClick(trip.id, p.id, p.name)}
                                                                        >
                                                                            <IconStar />
                                                                        </button>
                                                                    )}


                                                                    {isTripOwner(trip) && (
                                                                        <button
                                                                            type="button"
                                                                            className="iconBtn iconBtn--remove"
                                                                            title="Usuń uczestnika"
                                                                            aria-label="Usuń uczestnika"
                                                                            onClick={() =>
                                                                                setRemovePopup({
                                                                                    open: true,
                                                                                    tripId: trip.id,
                                                                                    participantId: p.id,
                                                                                    participantName: p.name,
                                                                                })
                                                                            }
                                                                        >
                                                                            <IconX />
                                                                        </button>
                                                                    )}
                                                                </>
                                                            )}
                                                        </div>

                                                    </div>
                                                ))}
                                            </div>
                                        </div>
                                    ) : (
                                        <div className="tripCard__photos">
                                            <div className="photoGrid">
                                                {trip.photos.map((ph) => (
                                                    <button
                                                        key={ph.id}
                                                        type="button"
                                                        className="photoTile"
                                                        title="Podgląd zdjęcia (placeholder)"
                                                        aria-label="Podgląd zdjęcia"
                                                    >
          <span className="photoTile__icon" aria-hidden="true">
            <IconImagePlaceholder />
          </span>
                                                    </button>
                                                ))}
                                            </div>

                                            <button
                                                type="button"
                                                className="fab"
                                                onClick={() => handleAddPhoto(trip.id)}
                                                aria-label="Dodaj zdjęcie"
                                                title="Dodaj zdjęcie"
                                            >
                                                <IconPlus />
                                            </button>
                                        </div>
                                    )}

                                </div>
                            )}
                        </section>
                    );
                })}
            </div>
            <ConfirmPopup
                open={removePopup.open}
                title="Czy na pewno chcesz usunąć tego użytkownika z podróży?"
                cancelLabel="Anuluj"
                confirmLabel="Usuń"
                onClose={() =>
                    setRemovePopup({
                        open: false,
                        tripId: null,
                        participantId: null,
                        participantName: "",
                    })
                }
                onConfirm={() => {
                    handleRemoveParticipant(removePopup.tripId, removePopup.participantId);
                    setRemovePopup({
                        open: false,
                        tripId: null,
                        participantId: null,
                        participantName: "",
                    });
                }}
            />

            <ProfileTripsAddParticipant
                open={addModal.open}
                trip={activeTrip}
                existingParticipantIds={existingIds}
                onClose={() => setAddModal({ open: false, tripId: null })}
                onAddUser={(user) => handleAddUserToTrip(user)}
            />

            <ReviewPopup
                open={reviewPopup.open}
                targetName={reviewPopup.targetName}
                onClose={() =>
                    setReviewPopup({
                        open: false,
                        tripId: null,
                        targetUserId: null,
                        targetName: "",
                    })
                }
                onSave={({ rating, text }) => {

                    setReviewPopup({
                        open: false,
                        tripId: null,
                        targetUserId: null,
                        targetName: "",
                    });

                    setReviewSuccessOpen(true);
                }}
            />

            <SuccessPopup
                open={reviewSuccessOpen}
                title="Opinia została wystawiona"
                buttonLabel="Okej"
                onClose={() => setReviewSuccessOpen(false)}
            />


        </div>

    );

}

function InfoItem({ label, icon, value, pill }) {
    return (
        <div className="infoItem">
            <div className="infoItem__label">
        <span className="infoItem__icon" aria-hidden="true">
          {icon}
        </span>
                {label}
            </div>
            {pill ? <span className="pill">{value}</span> : <div className="infoItem__value">{value}</div>}
        </div>
    );
}

function Svg({ children, size = 18 }) {
    return (
        <svg
            width={size}
            height={size}
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            strokeWidth="2"
            strokeLinecap="round"
            strokeLinejoin="round"
            aria-hidden="true"
        >
            {children}
        </svg>
    );
}

function IconChevron() {
    return (
        <Svg>
            <path d="M9 18l6-6-6-6" />
        </Svg>
    );
}

function IconPencil() {
    return (
        <Svg>
            <path d="M12 20h9" />
            <path d="M16.5 3.5a2.1 2.1 0 0 1 3 3L7 19l-4 1 1-4 12.5-12.5z" />
        </Svg>
    );
}

function IconTrash() {
    return (
        <Svg>
            <path d="M3 6h18" />
            <path d="M8 6V4h8v2" />
            <path d="M19 6l-1 14H6L5 6" />
            <path d="M10 11v6" />
            <path d="M14 11v6" />
        </Svg>
    );
}

function IconDot() {
    return (
        <Svg size={16}>
            <circle cx="12" cy="12" r="4" />
        </Svg>
    );
}

function IconPin() {
    return (
        <Svg>
            <path d="M12 21s7-4.5 7-11a7 7 0 0 0-14 0c0 6.5 7 11 7 11z" />
            <circle cx="12" cy="10" r="2" />
        </Svg>
    );
}

function IconCalendar() {
    return (
        <Svg>
            <path d="M8 2v4" />
            <path d="M16 2v4" />
            <path d="M3 8h18" />
            <path d="M5 4h14a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V6a2 2 0 0 1 2-2z" />
        </Svg>
    );
}

function IconTag() {
    return (
        <Svg>
            <path d="M20.59 13.41L11 3H4v7l10.59 10.59a2 2 0 0 0 2.82 0l3.18-3.18a2 2 0 0 0 0-2.82z" />
            <circle cx="7.5" cy="7.5" r="1.5" />
        </Svg>
    );
}

function IconClock() {
    return (
        <Svg>
            <circle cx="12" cy="12" r="9" />
            <path d="M12 7v6l4 2" />
        </Svg>
    );
}

function IconWallet() {
    return (
        <Svg>
            <path d="M3 7h18v12H3z" />
            <path d="M3 7l2-3h14l2 3" />
            <path d="M16 13h5" />
        </Svg>
    );
}

function IconCrown() {
    return (
        <Svg>
            <path d="M3 8l4.5 4L12 6l4.5 6L21 8v10H3V8z" />
        </Svg>
    );
}

function IconStar() {
    return (
        <Svg>
            <path d="M12 2l3 7 7 .6-5.3 4.6 1.7 7.2L12 17.8 5.6 21.4l1.7-7.2L2 9.6 9 9l3-7z" />
        </Svg>
    );
}

function IconList() {
    return (
        <Svg>
            <path d="M8 6h13" />
            <path d="M8 12h13" />
            <path d="M8 18h13" />
            <path d="M3 6h.01" />
            <path d="M3 12h.01" />
            <path d="M3 18h.01" />
        </Svg>
    );
}

function IconImages() {
    return (
        <Svg>
            <rect x="3" y="5" width="18" height="14" rx="2" />
            <path d="M8 13l2-2 4 4" />
            <path d="M13 12l2-2 3 3" />
            <circle cx="9" cy="9" r="1" />
        </Svg>
    );
}

function IconImagePlaceholder() {
    return (
        <Svg size={22}>
            <rect x="4" y="6" width="16" height="12" rx="2" />
            <path d="M8 14l2-2 4 4" />
            <circle cx="9" cy="10" r="1" />
        </Svg>
    );
}

function IconPlus() {
    return (
        <Svg>
            <path d="M12 5v14" />
            <path d="M5 12h14" />
        </Svg>
    );
}
function IconX() {
    return (
        <Svg>
            <path d="M18 6L6 18" />
            <path d="M6 6l12 12" />
        </Svg>
    );
}
