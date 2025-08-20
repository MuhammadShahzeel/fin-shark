import React, {
  type ChangeEvent,
  type SyntheticEvent,
  type JSX,
} from "react";
import { FaSearch } from "react-icons/fa";

interface Props {
  onSearchSubmit: (e: SyntheticEvent) => void;
  search: string | undefined;
  handleSearchChange: (e: ChangeEvent<HTMLInputElement>) => void;
}

const Search: React.FC<Props> = ({
  onSearchSubmit,
  search,
  handleSearchChange,
}: Props): JSX.Element => {
  return (
    <section className="relative bg-gray-100 py-6">
      <div className="max-w-2xl mx-auto px-4">
        <form
          onSubmit={onSearchSubmit}
          className="relative flex items-center bg-white rounded-full shadow-md border border-gray-200 focus-within:ring-2 focus-within:ring-lightGreen transition-all"
        >
          {/* Search Icon inside input */}
          <FaSearch className="absolute left-4 text-gray-400 text-lg" />

          {/* Input */}
          <input
            type="text"
            id="search-input"
            className="w-full pl-12 pr-5 py-3 rounded-full text-gray-800 placeholder-gray-400 focus:outline-none"
            placeholder="Search companies..."
            value={search}
            onChange={handleSearchChange}
          />
        </form>
      </div>
    </section>
  );
};

export default Search;
