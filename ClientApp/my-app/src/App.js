import './App.css';
import React from "react";
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Header from "./Componet/Header/Header";
import PagePermission from './Pages/AboutUs/PagePermission';
import Cart from './Pages/Cart/Cart';
import Memo from './Pages/Memo/Memo';
import Role from './Pages/Role/Role';
import Pagelist from './Pages/PageList';

function App() {
    return (
        <>
            <BrowserRouter>
                <div className="App">
                    <Header />
                    <Routes>
                        <Route exact path="/PagePermission" element={<PagePermission />} />
                        <Route exact path="/Cart" element={<Cart />} />
                        <Route exact path="/Memo" element={<Memo />} />
                        <Route exact path="/Role" element={<Role />} />
                        <Route exact path="/PageList" element={<Pagelist />} />
                    </Routes>
                </div>
            </BrowserRouter>
        </>
    );
}

export default App;