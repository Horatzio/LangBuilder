import { INavLink, Nav } from "@fluentui/react";
import { Outlet, useNavigate } from "react-router-dom";

const navGroups = [
  {
    links: [
      {
        name: "Home",
        url: "/",
      },
      {
        name: "Dashboard",
        url: "/dashboard",
      },
    ],
  },
];

const Layout: React.FC = () => {
  const navigate = useNavigate();

  const onLinkClick = (ev?: React.MouseEvent<HTMLElement>, item?: INavLink) => {
    if (!ev) return;
    ev.preventDefault();
    navigate(item?.url || "/");
  };

  return (
    <>
      <Nav groups={navGroups} onLinkClick={onLinkClick} />
      <Outlet />
    </>
  );
};
export default Layout;
