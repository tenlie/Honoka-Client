using UnityEngine;
using System.Xml;

public class QueryXmlParser : MonoBehaviour
{
    //싱글톤 패턴
    private static QueryXmlParser _instance;
    public static QueryXmlParser Instance { get { return _instance; } }

    //Sql파일명
    private string fileName = "/SqlProperties";
    public XmlDocument _queryXml;

	void Start ()
    {
        _queryXml.LoadXml(fileName);
	}
	
}
