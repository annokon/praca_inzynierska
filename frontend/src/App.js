import {BrowserRouter, Routes, Route} from "react-router-dom";
import './App.css';
import Login from "./Login";
import Register from "./Register";
import Navbar from "./components/Navbar/Navbar";
import { AuthProvider } from "./context/AuthContext";

function App() {
    return (
        <AuthProvider> {}
            <BrowserRouter>
                <Navbar />
                <Routes>
                    <Route path="/login" element={<Login/>}/>
                    <Route path="/register" element={<Register/>}/>
                </Routes>
            </BrowserRouter>
        </AuthProvider>
    );
}

export default App;
