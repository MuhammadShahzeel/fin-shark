import type { SyntheticEvent } from "react";
import { FiTrash } from "react-icons/fi";

interface Props {
  onPortfolioDelete: (e: SyntheticEvent) => void;
  portfolioValue: string;
}

const DeletePortfolio = ({ onPortfolioDelete, portfolioValue }: Props) => {
  return (
    <form onSubmit={onPortfolioDelete}>
      {/* Hidden input to pass portfolio symbol */}
      <input type="hidden" value={portfolioValue} readOnly />

      <button
        type="submit"
        title={`Delete ${portfolioValue}`}
        className="p-2 text-red-500 rounded-full hover:bg-red-100 hover:text-red-600 transition-colors"
      >
        <FiTrash size={20} />
      </button>
    </form>
  );
};

export default DeletePortfolio;
