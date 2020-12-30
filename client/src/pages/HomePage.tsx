import React from "react";
import { Link } from "react-router-dom"


const HomePage: React.FC = () => (
    <>
        <h1>Hello word</h1>
        <Link to="/auth">login page</Link>
    </>
)

export default HomePage;
