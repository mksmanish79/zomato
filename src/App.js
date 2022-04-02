import Header from "./Global/Header";
import Home from './Pages/Home';
import Partner from './Pages/Partner';
import Login from './Pages/Login';
import Footer from "./Global/Footer";
import React from 'react';
import {
  BrowserRouter as Router,
  Routes,
  Route
} from "react-router-dom";

function App() {
  return (
    <>
      <Header />
      <Router>
        <Routes>
          <Route path="/" element={<Home />}></Route>
          <Route path="/partner" element={<Partner />}></Route>
          <Route path="/login" element={<Login />}></Route>
        </Routes>
      </Router>
      <Footer />
    </>
  );
}

export default App;
