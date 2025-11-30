import {BrowserRouter, Routes, Route} from "react-router-dom";
import './App.css';
import Login from "./Login";
import Register from "./Register";
import Navbar from "./components/Navbar/Navbar";

function App() {
    return (
        <BrowserRouter>
            <Navbar />
            <Routes>
                <Route path="/login" element={<Login/>}/>
                <Route path="/register" element={<Register/>}/>
            </Routes>
        </BrowserRouter>
    );
}

// function App() {
//     return (
//         <>
//             <Navbar />
//             {/* tutaj będzie cały Twój content */}
//         </>
//     );
// }

export default App;
