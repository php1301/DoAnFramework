//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources.Search.Views {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class SearchResults {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal SearchResults() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("OJS.Web.App_GlobalResources.Search.Views.SearchResults", typeof(SearchResults).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Contests.
        /// </summary>
        public static string Contests {
            get {
                return ResourceManager.GetString("Contests", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to from contest.
        /// </summary>
        public static string From_contest {
            get {
                return ResourceManager.GetString("From_contest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Problems.
        /// </summary>
        public static string Problems {
            get {
                return ResourceManager.GetString("Problems", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The search term must be at least {0} characters..
        /// </summary>
        public static string Search_term_too_short {
            get {
                return ResourceManager.GetString("Search_term_too_short", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Search results for &quot;{0}&quot;.
        /// </summary>
        public static string Title {
            get {
                return ResourceManager.GetString("Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Users.
        /// </summary>
        public static string Users {
            get {
                return ResourceManager.GetString("Users", resourceCulture);
            }
        }
    }
}
