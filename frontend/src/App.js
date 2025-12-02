import {BrowserRouter, Routes, Route} from "react-router-dom";
import './App.css';
import Login from "./views/Login";
import Register from "./views/Register";
import Navbar from "./components/Navbar/Navbar";
import { AuthProvider } from "./context/AuthContext";
import VerifyEmail from "./views/VerifyEmail";
import AdditionalInfo from "./views/AdditionalInfo";
import VerifiedEmail from "./views/VerifiedEmail";

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
                </Routes>
            </BrowserRouter>
        </AuthProvider>
    );
}

export default App;
