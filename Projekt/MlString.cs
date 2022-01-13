using System;
using System.Resources;
using System.Text;
using System.Reflection;
using System.Globalization;

namespace MultiLang
{
  internal class ml
  {
        public static string[] SupportedCultures = { "en", "hu" };  //MLHIDE
        private static string RootNamespace = "Projekt"; //MLHIDE
    private static ResourceManager   ResMgr ;

    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance", "CA1810:Initialize reference type static fields inline", Justification = "Uses program code" )]
    static ml()
    {
      if ( RootNamespace.Length > 0 )
        ResMgr = new ResourceManager ( RootNamespace + ".MultiLang", Assembly.GetExecutingAssembly ( ) ) ; //MLHIDE
      else
        ResMgr = new ResourceManager ( "MultiLang", Assembly.GetExecutingAssembly ( ) );                   //MLHIDE
    }

    public static void ml_UseCulture ( CultureInfo ci )
    {
      System.Threading.Thread.CurrentThread.CurrentUICulture = ci ;
    }
    
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Usage", "CA1801:Review unused parameters", Justification = "Parameter is not used by design." )]
    public static string ml_string ( int StringID, string Text )
    {
      return ml_resource ( StringID ) ;
    }

    public static string ml_resource ( int StringID )
    {
      return ResMgr.GetString ( "_" + StringID.ToString(CultureInfo.InvariantCulture) ) ;                  //MLHIDE
    }
  }
}
