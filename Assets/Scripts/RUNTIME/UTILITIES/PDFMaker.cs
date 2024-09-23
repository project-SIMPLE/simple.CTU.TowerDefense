using System.Collections;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp;
using UnityEngine;

public class PDFMaker : MonoBehaviour
{
    [SerializeField] PlayerResourcesManager playerResourcesManager;
    [SerializeField] SubsidenceManager subsidenceManager;
    
    public static PDFMaker Instance = null;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    public void MakePDF()
    {
        var reportText = "Living Trees: " + playerResourcesManager.CurrentRefillSources + "\n" +
                         "Dead Trees: " + (playerResourcesManager.TotalTree - playerResourcesManager.CurrentRefillSources) + "\n" +
                         "Lake Structures Built: " + StatisticsManager.Instance.LakeCount + "\n" +
                         "WaterPump Structures Built: " + StatisticsManager.Instance.WaterPumpCount + "\n" +
                         "SluiceGate Structures Built: " + StatisticsManager.Instance.SluiceGateCount + "\n" +
                         "Enemies Neutralized: " + StatisticsManager.Instance.EnemyKillCount + "\n" +
                         "Remaining Groundwater Level (Local): " + subsidenceManager.RemainingWaterLevelLocal + "\n" +
                         "Remaining Groundwater Level (Global): " + subsidenceManager.RemainingWaterLevelGlobal + "\n" +
                         "Subsidence Score: " + subsidenceManager.SubsidenceScore;

        Document doc = new Document(PageSize.A4);
        PdfWriter wri = PdfWriter.getInstance(doc, new FileStream("Report.pdf", FileMode.Create));
        doc.Open();

        Paragraph par = new Paragraph("AQUADEFENDERS: VILLAGE VANGUARD\nGAME REPORT: \n\n"+reportText+"\n\nHISTORY:");
        doc.Add(par);

        PdfPTable table = new PdfPTable(4);
        table.addCell("Time"); table.addCell("Action"); table.addCell("Construction"); table.addCell("Position");

        foreach (ActionHistory e in StatisticsManager.Instance.histories)
        {
            string position = e.position.x.ToString("F2") + ", " + e.position.y.ToString("F2");
            table.addCell(e.datetime); table.addCell(e.action); table.addCell(e.construction); table.addCell(position);
        }
        doc.Add(table);
        doc.Close();
    }

}
