import React, { useState } from "react";
import { Link } from "react-router-dom";
import { HiOutlineMenu, HiOutlineX } from "react-icons/hi"; // React Icons
import logo from "../../assets/logo.png";
import { useAuth } from "../../context/useAuth";

interface Props {}

const Navbar = (props: Props) => {
  const { isLoggedIn, user, logout } = useAuth();
  const [isOpen, setIsOpen] = useState(false);

  return (
    <nav className="relative container mx-auto p-6">
      <div className="flex items-center justify-between">
        {/* Logo + Desktop Links */}
        <div className="flex items-center space-x-20">
          <Link to="/">
            <img src={logo} alt="Logo" className="h-10" />
          </Link>
          <div className="hidden font-bold lg:flex space-x-6">
            <Link to="/search" className="text-black hover:text-darkBlue">
              Search
            </Link>
            <Link to="/portfolio" className="text-black hover:text-darkBlue">
              Portfolio
            </Link>
          </div>
        </div>

        {/* Desktop Right Side */}
        {isLoggedIn() ? (
          <div className="hidden lg:flex items-center space-x-6 text-black">
            <span className="hover:text-darkBlue">
              Welcome, {user?.userName}
            </span>
            <button
              onClick={logout}
              className="px-6 py-2 font-bold rounded text-white bg-lightGreen hover:opacity-70"
            >
              Logout
            </button>
          </div>
        ) : (
          <div className="hidden lg:flex items-center space-x-6 text-black">
            <Link to="/login" className="hover:text-darkBlue">
              Login
            </Link>
            <Link
              to="/register"
              className="px-6 py-2 font-bold rounded text-white bg-lightGreen hover:opacity-70"
            >
              Signup
            </Link>
          </div>
        )}

        {/* Mobile Hamburger */}
        <div className="lg:hidden">
          <button onClick={() => setIsOpen(!isOpen)}>
            {isOpen ? <HiOutlineX size={28} /> : <HiOutlineMenu size={28} />}
          </button>
        </div>
      </div>

      {/* Mobile Dropdown Menu */}
      {isOpen && (
        <div className="lg:hidden mt-4 space-y-4 flex flex-col items-center font-bold text-black bg-gray-100 rounded-lg p-4 shadow-md">
          <Link to="/search" onClick={() => setIsOpen(false)}>
            Search
          </Link>
          <Link to="/portfolio" onClick={() => setIsOpen(false)}>
            Portfolio
          </Link>

          {isLoggedIn() ? (
            <>
              <span>Welcome, {user?.userName}</span>
              <button
                onClick={() => {
                  logout();
                  setIsOpen(false);
                }}
                className="w-full px-6 py-2 font-bold rounded text-white bg-lightGreen hover:opacity-70"
              >
                Logout
              </button>
            </>
          ) : (
            <>
              <Link to="/login" onClick={() => setIsOpen(false)}>
                Login
              </Link>
              <Link
                to="/register"
                onClick={() => setIsOpen(false)}
                className="w-full px-6 py-2 font-bold rounded text-white bg-lightGreen hover:opacity-70 text-center"
              >
                Signup
              </Link>
            </>
          )}
        </div>
      )}
    </nav>
  );
};

export default Navbar;
