import {BrowserRouter, Routes, Route} from "react-router-dom";
import './App.css';
import Login from "./views/Login/Login";
import Register from "./views/Register/Register";
import Navbar from "./components/Navbar/Navbar";
import { AuthProvider } from "./context/AuthContext";
import VerifyEmail from "./views/Register/VerifyEmail";
import AdditionalInfo from "./views/Register/AdditionalInfo";
import VerifiedEmail from "./views/Register/VerifiedEmail";
import ForgotPassword from "./views/Login/ForgotPassword";
import MyProfile from "./views/MyProfile";
import UserProfile from "./views/UserProfile";
import ProfileLayout from "./views/ProfileLayout";

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
                    {/*<Route path="/profile" element={<ProfileLayout />}>*/}
                    {/*    <Route index element={<AboutTab />} />*/}
                    {/*    <Route path="o-uzytkowniku" element={<AboutTab />} />*/}
                    {/*    <Route path="podroze" element={<TripsTab />} />*/}
                    {/*    <Route path="opinie" element={<ReviewsTab />} />*/}
                    {/*    <Route path="osiagniecia" element={<AchievementsTab />} />*/}
                    {/*    <Route path="filtry" element={<FiltersTab />} />*/}
                    <Route path="/profile" element={<MyProfile />} />
                    <Route path="/u/:id" element={<UserProfile />} />
                </Routes>
            </BrowserRouter>
        </AuthProvider>
    );
}

export default App;
