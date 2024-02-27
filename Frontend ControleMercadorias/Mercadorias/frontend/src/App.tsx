import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './components/Login/login';
import Form from './pages/FormList';
import TelaInicial from './components/TelaInicial/TelaInicial';
import FormCRUD from './pages/FormCRUD';

const App: React.FC = () => (
  <Router>
    <Routes>
      <Route path="/" element={<TelaInicial />} />
      <Route path="/login" element={<Login />} />
      <Route path="/formulario" element={<FormCRUD />} />
      <Route path="/formList" element={<Form />} />
    </Routes>
  </Router>
);

export default App;
