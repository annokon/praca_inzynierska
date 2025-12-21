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
import ProfileLayout from "./views/ProfileLayout";
import ProfileAbout from "./views/ProfileAbout";
import ProfileReview from "./views/ProfileReview";

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
                        {/*<Route path="podroze" element={<ProfileTrips />} />*/}
                        <Route path="opinie" element={<ProfileReview />} />
                        {/*<Route path="osiagniecia" element={<ProfileAchievements />} />*/}
                        {/*<Route path="filtry" element={<ProfileFilters />} />*/}
                    </Route>
                </Routes>
            </BrowserRouter>
        </AuthProvider>
    );
}

export default App;
