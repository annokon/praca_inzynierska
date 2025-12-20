import { NavLink, Outlet } from "react-router-dom";

export default function ProfileLayout() {
    return (
        <div>
            <div className="tabs">
                <NavLink to="o-uzytkowniku" className="tab">O Użytkowniku</NavLink>
                <NavLink to="podroze" className="tab">Podróże</NavLink>
                <NavLink to="opinie" className="tab">Opinie</NavLink>
                <NavLink to="osiagniecia" className="tab">Osiągnięcia</NavLink>
                <NavLink to="filtry" className="tab">Moje Filtry Wyszukiwania</NavLink>
            </div>

            <Outlet />
        </div>
    );
}
