public void copiar_legenda()
{
	try 
	{
	Document doc1 = this.ActiveUIDocument.Document;
        Document doc2 = this.Application.Documents.Cast<Document>().First(a=>a.Title!=doc1.Title);
        CopyPasteOptions op = new CopyPasteOptions();
        ICollection<ElementId> col = new List<ElementId>();
        FilteredElementCollector coll = new FilteredElementCollector(doc1);
        coll = coll.WhereElementIsNotElementType().OfClass(typeof(View));
        var v = coll.First(a=> ((View)a).ViewType == ViewType.Legend);
        col.Add(v.Id);
        Transaction t = new Transaction(doc2,"copiar legenda");
        t.Start();
        ElementTransformUtils.CopyElements(doc1,col,doc2,null,op);
        t.Commit();
	} 
	catch{}
}
