import React, { type JSX, type SyntheticEvent } from "react";
import Card from "../Card/Card";
import type { CompanySearch } from "../../company";

interface Props {
  searchResults: CompanySearch[] | null;
  onPortfolioCreate: (e: SyntheticEvent) => void;
}

const CardList: React.FC<Props> = ({
  searchResults,
  onPortfolioCreate,
}: Props): JSX.Element => {
  if (!searchResults) {
    return <p className="text-center mt-6 text-gray-500">Search for stocks above</p>;
  }

  if (searchResults.length === 0) {
    return <p className="text-center mt-6 text-gray-500">No stocks found!</p>;
  }

  return (
    <div className="grid gap-4 mt-6 md:grid-cols-2 lg:grid-cols-3">
      {searchResults.map((result) => (
        <Card
          key={result.symbol}
          id={result.symbol}
          searchResult={result}
          onPortfolioCreate={onPortfolioCreate}
        />
      ))}
    </div>
  );
};

export default CardList;
