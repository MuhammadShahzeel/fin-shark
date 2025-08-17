import React from "react";
import "./Spinner.css";
import { ClipLoader } from "react-spinners";

type Props = {
  isLoading?: boolean;
};

const Spinner = ({ isLoading = true }: Props) => {
  if (!isLoading) return null;

  return (
    <div id="loading-overlay">
      <div id="loading-spinner">
        <ClipLoader
          color="#36d7b7"
          loading={isLoading}
          size={45}
          aria-label="Loading Spinner"
          data-testid="loader"
        />
        <p className="loading-text">Loading...</p>
      </div>
    </div>
  );
};

export default Spinner;
