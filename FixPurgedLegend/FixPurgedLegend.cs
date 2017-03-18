using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;

namespace FixPurgedLegend
{
    [Transaction(TransactionMode.Manual)]
    internal class FixPurgedLegend : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, Autodesk.Revit.DB.ElementSet elements)
        {
		try 
		{
			UIApplication uiapp = commandData.Application;
			Document sourceDocument = uiapp.ActiveUIDocument.Document;
			Document destinationDocument = uiapp.Application.Documents.Cast<Document>().First(a=>a.Title!=sourceDocument.Title);
			CopyPasteOptions options = new CopyPasteOptions();
			FilteredElementCollector col = new FilteredElementCollector(sourceDocument);
			col = col.WhereElementIsNotElementType().OfClass(typeof(View));
			ElementId id = col.First(a=> ((View)a).ViewType == ViewType.Legend).Id;
			ICollection<ElementId> elementsToCopy = new List<ElementId>();
			elementsToCopy.Add(id);
			Transaction transaction = new Transaction(destinationDocument, "FixPurgedLegend");
			transaction.Start();
			ElementTransformUtils.CopyElements(sourceDocument, elementsToCopy, destinationDocument, null,options);
			transaction.Commit();
			transaction.Dispose();
		} 
		catch (Exception ex)
		{
		message = ex.Message + "\n" + ex.StackTrace;
		return Result.Failed;
		}

            return Result.Succeeded;
        }
    }
}
