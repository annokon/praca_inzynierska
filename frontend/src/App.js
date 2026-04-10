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
import SettingsEditData from "./views/Settings/SettingsEditData/SettingsEditData";
import SettingsAppearance from "./views/Settings/SettingsProfile/SettingsAppearance";
import SettingsCurrency from "./views/Settings/SettingsProfile/SettingsCurrency";
import SettingsEditUsername from "./views/Settings/SettingsEditData/SettingsEditUsername";
import SettingsEditDisplayName from "./views/Settings/SettingsEditData/SettingsEditDisplayName";
import SettingsEditBirthDate from "./views/Settings/SettingsEditData/SettingsEditBirthdate";
import SettingsEditLanguages from "./views/Settings/SettingsEditData/SettingsEditLanguages";
import SettingsEditGender from "./views/Settings/SettingsEditData/SettingsEditGender";

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
                        <Route index element={<Navigate to="about-user" replace />} />
                        <Route path="about-user" element={<ProfileAbout />} />
                        {<Route path="trips" element={<ProfileTrips />} />}
                        <Route path="reviews" element={<ProfileReview />} />
                        <Route path="achievements" element={<ProfileAchievements />} />
                    </Route>
                    <Route path="/fav-filters" element={<TripsFavFilters />} />
                    <Route element={<RequireAuth />}>
                        <Route path="/settings-profile" element={<SettingsProfile />} />
                        <Route path="/settings-edit-data" element={<SettingsEditData />} />
                        <Route path="/settings-info" element={<SettingsChangeAdditionalnfo />} />
                        <Route path="/settings-appearance" element={<SettingsAppearance />} />
                        <Route path="/settings-currency" element={<SettingsCurrency />} />
                        <Route path="/settings-edit-username" element={<SettingsEditUsername />} />
                        <Route path="/settings-edit-display-name" element={<SettingsEditDisplayName />} />
                        <Route path="/settings-edit-birthdate" element={<SettingsEditBirthDate />} />
                        <Route path="/settings-edit-languages" element={<SettingsEditLanguages />} />
                        <Route path="/settings-edit-gender" element={<SettingsEditGender />} />
                    </Route>
                </Routes>
            </BrowserRouter>
        </AuthProvider>
    );
}

export default App;
