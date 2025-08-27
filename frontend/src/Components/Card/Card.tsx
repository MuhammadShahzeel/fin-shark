import React, { type JSX, type SyntheticEvent } from "react";
import { Link } from "react-router-dom";
import type { CompanySearch } from "../../company";
import AddPortfolio from "../Portfolio/AddPortfolio/AddPortfolio";

interface Props {
  id: string;
  searchResult: CompanySearch;
  onPortfolioCreate: (e: SyntheticEvent) => void;
}

const Card: React.FC<Props> = ({ id, searchResult, onPortfolioCreate }: Props): JSX.Element => {
  return (
    <div className="flex flex-col justify-between w-full p-6 bg-white rounded-xl shadow-md hover:shadow-lg transition-shadow md:flex-row md:items-center">
      <div className="flex flex-col mb-3 md:mb-0">
        <Link
          to={`/company/${searchResult.symbol}/company-profile`}
          className="font-semibold text-lg text-gray-800 hover:text-gray-900"
        >
          {searchResult.name} ({searchResult.symbol})
        </Link>
        <p className="text-gray-500">{searchResult.currency}</p>
        <p className="text-gray-600 font-medium">
          {searchResult.exchange} - {searchResult.exchangeFullName}
        </p>
      </div>
      <AddPortfolio onPortfolioCreate={onPortfolioCreate} symbol={searchResult.symbol} />
    </div>
  );
};

export default Card;
