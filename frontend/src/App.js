import {BrowserRouter, Routes, Route, Navigate} from "react-router-dom";
import './App.css';
import Login from "./views/Login/Login";
import Register from "./views/Register/Register";
import Navbar from "./components/Navbar/Navbar";
import { AuthProvider } from "./context/AuthContext";
import VerifyEmail from "./views/Register/VerifyEmail";
import AdditionalInfo from "./views/Register/AdditionalInfo";
import VerifiedEmail from "./views/Register/VerifiedEmail";
import ForgotPassword from "./views/Login/ForgotPassword";
import ProfileLayout from "./views/Profile/ProfileLayout";
import ProfileAbout from "./views/Profile/ProfileAbout";
import ProfileReview from "./views/Profile/ProfileReview";
import ProfileTrips from "./views/Profile/ProfileTrips";
import ProfileAchievements from "./views/Profile/ProfileAchievements";
import TripsFavFilters from "./views/TripsFavFilters";
import SettingsChangeAdditionalnfo from "./views/Settings/SettingsChangeAdditionalInfo/SettingsChangeAdditionalnfo";
import RequireAuth from "./routing/RequireAuth";
import SettingsProfile from "./views/Settings/SettingsProfile/SettingsProfile";
import SettingsChangeData from "./views/Settings/SettingsChangeData/SettingsChangeData";

function App() {
    return (
        <AuthProvider> {}
            <BrowserRouter>
                <Navbar />
                <Routes>
                    <Route path="/login" element={<Login/>}/>
                    <Route path="/register" element={<Register/>}/>
                    <Route path="/verify-email" element={<VerifyEmail />} />
                    <Route path="/verified-email" element={<VerifiedEmail />} />
                    <Route path="/additional-info" element={<AdditionalInfo />} />
                    <Route path="/forgot-password" element={<ForgotPassword />} />

                    <Route path="/profile/:username" element={<ProfileLayout />}>
                        <Route index element={<Navigate to="o-uzytkowniku" replace />} />
                        <Route path="o-uzytkowniku" element={<ProfileAbout />} />
                        {<Route path="podroze" element={<ProfileTrips />} />}
                        <Route path="opinie" element={<ProfileReview />} />
                        <Route path="osiagniecia" element={<ProfileAchievements />} />
                    </Route>
                    <Route path="/fav-filters" element={<TripsFavFilters />} />
                    <Route element={<RequireAuth />}>
                        <Route path="/settings-profile" element={<SettingsProfile />} />
                        <Route path="/settings-change-data" element={<SettingsChangeData />} />
                        <Route path="/settings-info" element={<SettingsChangeAdditionalnfo />} />
                    </Route>
                </Routes>
            </BrowserRouter>
        </AuthProvider>
    );
}

export default App;
