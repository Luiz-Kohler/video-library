import React from 'react';
import { Route, Routes } from 'react-router-dom'

import Home from './pages/home'
import Clientes from './pages/clientes'
import Filmes from './pages/filmes'
import Locacoes from './pages/locacoes'

const PageRoutes = () => {
    return (
        <Routes>
            <Route path="/" element={<Home />}/>
            <Route path="/clientes" element={<Clientes />}/>
            <Route path="/filmes" element={<Filmes />}/>
            <Route path="/locacoes" element={<Locacoes />}/>
        </Routes>
    )
}

export default PageRoutes;