export function downloadAsJson(fileName: string, content: object) {
  if (!fileName) {
    throw new Error("Unable download file without name!");
  }

  const url = window.URL.createObjectURL(
    new Blob([JSON.stringify(content)], {
      type: "application/json",
    })
  );

  const link = document.createElement("a");
  link.href = url;
  link.setAttribute("download", `${fileName}.json`);

  document.body.appendChild(link);
  link.click();
  link.parentNode?.removeChild(link);
}
