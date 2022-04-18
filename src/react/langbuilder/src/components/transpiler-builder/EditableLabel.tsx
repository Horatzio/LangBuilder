import { useState } from "react";
import { AiOutlineEdit } from "react-icons/ai";
import { TiTick } from "react-icons/ti";
import { ImCross } from "react-icons/im";

interface EditableLabelProps {
  value: string;
  placeholder?: string;
  onValueChange: (newValue: string) => void;
}

const EditableLabel: React.FC<EditableLabelProps> = ({
  value,
  placeholder,
  onValueChange,
}) => {
  const [text, setText] = useState(value);
  const [isEditing, setIsEditing] = useState(false);
  const [isHovering, setIsHovering] = useState(false);

  const onSave = () => {
    setIsEditing(false);
    onValueChange(text);
  };

  const onDiscard = () => {
    setIsEditing(false);
    setText(value);
  };

  return (
    <div className="relative m-5">
      {isEditing ? (
        <div className="w-fit flex flex-row text-sm border-4 border-dashed border-gray-200 rounded-lg">
          <input
            className="w-fit object-contain py-2 pl-3 pr-1 focus:outline-none"
            id="email"
            value={text}
            placeholder={placeholder}
            onChange={(e) => setText(e.target.value)}
          />
          <TiTick className="m-1 w-6 h-6" onClick={onSave} />
          <ImCross className="m-2 w-4 h-4 p-0.25" onClick={onDiscard} />
        </div>
      ) : (
        <div
          className={
            (isHovering ? "border-2 border-cyan-400 rounded-lg" : "") +
            " w-sm flex flex-row"
          }
          onMouseEnter={() => setIsHovering(true)}
          onMouseLeave={() => setIsHovering(false)}
        >
          <label className="focus:outline-none cursor-pointer p-1">
            {text || placeholder}
          </label>
          <AiOutlineEdit
            className={
              (isHovering ? "" : "invisible") +
              " mt-1 p-1 w-7 h-7 cursor-pointer"
            }
            onClick={() => setIsEditing(true)}
          />
        </div>
      )}
    </div>
  );
};

export default EditableLabel;
