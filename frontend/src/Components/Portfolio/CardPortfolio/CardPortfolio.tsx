import type { SyntheticEvent } from "react";
import { Link } from "react-router-dom";
import DeletePortfolio from "../DeletePortfolio/DeletePortfolio";
import type { PortfolioGet } from "../../../models/Portfolio";

interface Props {
  portfolioValue: PortfolioGet;
  onPortfolioDelete: (e: SyntheticEvent) => void;
}

const CardPortfolio = ({ portfolioValue, onPortfolioDelete }: Props) => {
  return (
    <div className="flex flex-col w-full p-6 rounded-2xl shadow-md bg-white hover:shadow-lg transition-shadow md:w-1/3">
      <div className="flex items-center justify-between">
        {/* Portfolio symbol link */}
        <Link
          to={`/company/${portfolioValue.symbol}/company-profile`}
          className="text-2xl font-semibold text-gray-800 hover:text-gray-900 transition-colors"
        >
          {portfolioValue.symbol}
        </Link>

        {/* Delete button */}
        <DeletePortfolio
          portfolioValue={portfolioValue.symbol}
          onPortfolioDelete={onPortfolioDelete}
        />
      </div>

      {/* Small note under */}
      <p className="mt-3 text-sm text-gray-500">
        Click to view company profile
      </p>
    </div>
  );
};

export default CardPortfolio;
