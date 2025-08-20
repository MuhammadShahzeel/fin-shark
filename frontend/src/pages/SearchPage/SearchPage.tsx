import React, { useState, type ChangeEvent, type SyntheticEvent } from "react";
import { searchCompanies } from "../../api";
import Search from "../../Components/Search/Search";
import CardList from "../../Components/CardList/CardList";
import { toast } from "react-toastify";
import type { CompanySearch } from "../../company";
import { portfolioAddAPI } from "../../services/PortfolioService";

const SearchPage: React.FC = () => {
  const [search, setSearch] = useState<string>("");
  const [searchResult, setSearchResult] = useState<CompanySearch[] | null>(null);
  const [serverError, setServerError] = useState<string | null>(null);

  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
  };

  const onPortfolioCreate = (e: SyntheticEvent) => {
    e.preventDefault();
    const target = e.target as HTMLFormElement;
    const symbol = (target[0] as HTMLInputElement).value;

    portfolioAddAPI(symbol)
      .then((res) => {
        if (res?.status === 204) {
          toast.success("Stock added to portfolio!");
        }
      })
      .catch(() => {
        toast.warning("Could not add stock to portfolio!");
      });
  };

  const onSearchSubmit = async (e: SyntheticEvent) => {
    e.preventDefault();
    const result = await searchCompanies(search);
    if (typeof result === "string") {
      setServerError(result);
      setSearchResult([]);
    } else if (Array.isArray(result.data)) {
      setSearchResult(result.data);
    }
  };

  return (
    <div className="container mx-auto p-4">
      <Search
        onSearchSubmit={onSearchSubmit}
        search={search}
        handleSearchChange={handleSearchChange}
      />

      {serverError && (
        <p className="text-center text-red-500 mt-4">
          Unable to connect to API
        </p>
      )}

      <CardList
        searchResults={searchResult}
        onPortfolioCreate={onPortfolioCreate}
      />
    </div>
  );
};

export default SearchPage;
