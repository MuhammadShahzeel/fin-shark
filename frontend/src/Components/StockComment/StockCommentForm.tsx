import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";

type Props = {
  symbol: string;
  handleComment: (e: CommentFormInputs) => void;
};

type CommentFormInputs = {
  title: string;
  content: string;
};

const validation = Yup.object().shape({
  title: Yup.string().required("Title is required"),
  content: Yup.string().required("Content is required"),
});

const StockCommentForm = ({ symbol, handleComment }: Props) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<CommentFormInputs>({ resolver: yupResolver(validation) });

  return (
    <form className="mt-4 ml-4" onSubmit={handleSubmit(handleComment)}>
      <input
        type="text"
        id="title"
        className="mb-3 bg-white border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-lightGreen focus:border-lightGreen block w-full p-2.5"
        placeholder="Title"
        {...register("title")}
      />
      {errors.title && <p className="text-red-500 text-sm">{errors.title.message}</p>}

      <div className="py-2 px-4 mb-4 bg-white rounded-lg border border-gray-200">
        <label htmlFor="comment" className="sr-only">
          Your comment
        </label>
        <textarea
          id="comment"
          className="px-0 w-full min-h-[8rem] text-sm text-gray-900 border-0 focus:ring-0 focus:outline-none resize-none"
          placeholder="Write a comment..."
          {...register("content")}
        ></textarea>
      </div>
      {errors.content && <p className="text-red-500 text-sm">{errors.content.message}</p>}

      <button
        type="submit"
        className="w-full text-white bg-lightGreen hover:opacity-80 focus:ring-4 focus:ring-lightGreen/50 font-medium rounded-lg text-sm px-5 py-2.5"
      >
        Post comment
      </button>
    </form>
  );
};

export default StockCommentForm;
