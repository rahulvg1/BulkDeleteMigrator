using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace BulkDeleteMigrator
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "Bulk Delete Migrator"),
        ExportMetadata("Description", "Helps you to Migrate (clone) Bulk Deletion jobs between Dataverse environments"),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAFRUlEQVR4Xu2bTWxbRRDHd99L7AapEioKAkQPRYAQBSo7rmrHSFQVqlQVlQNy1SR2qZPS5gBO2wtCcKXqqVICl4Q0Cf5IUSMOjcoFOAQJOwn5qMJHDuFQUFGQQEFIqYid5HmYt8ZF5MPxrP32mTi52If9v5n5efZj3k44q/I/XuXxsx0AVmZA13S4noExhZj3ku0AWwBt1dfhGvyRrCUILM+AD2ZC+8BgY4zxhwl+5YfOG9ms56In/quEtiiJ5QBML7qmW54F0Mc4Z7uL8uq/g+ZWnUu+S/uH/pDQbilRAsD04sPp0z6DwQhn3LGlV+sGwMxqfdp3ae/QEl1bWKEMgMiE28HjLMtvMs51eiAw4szWHT3v6VmhazdXKAVgutE5HTqFWXBdJggAdivijp7AqQQy+o00ygHk1oRQBBfFTqkgAAYiDbGwlHYDkS0AchBOX8GPt2UCAQZXOtyxd2S0azW2ARAQpkL9uB6ckQsEOiLuWJec9l+VrQBwTnPMhGGc06/IBIKZ0ISZ8ImMNq+xFYDpRPfkudqMtvQ5rgmHyYEAHrE0eDXiin9G1v4jsB2A6cfVu4G6mt93jSKEA9RAMAuWdYT3pjuKevpfRQAQEH4I7NHTuyY4509Qw8CptIj74osXGqLfUrUVA0AsijMtjzNDH8evj1EDwZPBAq+Bg28diN2haCsKgOl45+3mpxjUfIOOPUgJRIwFdteA7CFK8SQAdE6FvsPUe45ssCIFMGtw3X/RNfBnMe4JALgVfY8f+4sR/B/G4MI4YdSnXyqmeNqWAHI/Eow84kq/fJIPGYV+tG0MwFwS4NOIKxYoVDxtawC5RChcPG1/ALnd4XKkIfruRlOhOgDkILQjhO61EKoHAM4FPC02ry2eqgiAWA+MrAbHLrjiX+QzoboA5KZCWuPsSL54qj4AIhHYIueGN+JOzFZcLaD6pLkDQDXxSrO3kwGV9ouo9kdkQEt/6nm8rXlItXE77WmaNh99/dCcAND08dgL+qrxNb6jl7m9tTMOadt4KuxOtDa2318Dmq4lfRrneHvLJG5vpf2wTbgOgOlJsDd5HDR2E6eDxO2tbbFIGd4QgPmk5t7kKU2Tu72V8sQm0aYABIS+ZESTvb21KSCq2YIAxM7Ql7qM60FZbmCpzqkYvyUAAeFash9fl59R4ZBqG0UBwLKJB/tSQ7g9vqbaQavtFQcAvQjcAN15b/RL/HrYaqdUPr9oAKZTgRupOsc99hWuCQdVOmmlLRIAAaE3tcehsSRCeEbGMXwJ8TO+n/9JRmuJBmA43ua/SqoGg7HJR2ElM44HJXrrK2MLWa75BsNeS1tfqbBIAMyHhz4a3we6MYFfycUTvped5w6nJx7yWNb6ajkA00BJxRPAXAa4b+hsoyWtr0oACAilFE8AM5ndCOFkY9lbX5UBMA2VWDyNPLDiONpz3lPW1lelAExjpRRPuBXdSoR9J/CgVbbWV+UABIQSiidcGAcSbf4w1fFyjSfvApsZDvYlsfWVy7a+vp9o9b9XrqAozykbANNoicVTe7y1cd3tLSUYmbFlBWAWTy39o8P4UHLrKy4CAFloHjzrL6n1lQqhvADQ+rnuydq/apex9ZVePGFLi4H/S3EsHvbev72lBkQdX3YApgNm8eRchFFc3cmtr5gIaQPYkettfqnW14oAICBg8eTkAsLTVKdwMiwC172JVu8sWUsUWJIBeR9E8bScmcS3SvTWVwa/gaE3JN7w/kKMiTTcUgCmJ8He1JPAoQchaCTPcDCeEe5YfUawHAA1aNXjdwCoJl5p9v4GJ5fEUMdwPdQAAAAASUVORK5CYII="),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "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"),
        ExportMetadata("BackgroundColor", "Lavender"),
        ExportMetadata("PrimaryFontColor", "Black"),
        ExportMetadata("SecondaryFontColor", "Gray")]
    public class MyPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new BulkDeleteMigrator();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public MyPlugin()
        {
            // If you have external assemblies that you need to load, uncomment the following to 
            // hook into the event that will fire when an Assembly fails to resolve
            // AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);
        }

        /// <summary>
        /// Event fired by CLR when an assembly reference fails to load
        /// Assumes that related assemblies will be loaded from a subfolder named the same as the Plugin
        /// For example, a folder named Sample.XrmToolBox.MyPlugin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly loadAssembly = null;
            Assembly currAssembly = Assembly.GetExecutingAssembly();

            // base name of the assembly that failed to resolve
            var argName = args.Name.Substring(0, args.Name.IndexOf(","));

            // check to see if the failing assembly is one that we reference.
            List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
            var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

            // if the current unresolved assembly is referenced by our plugin, attempt to load
            if (refAssembly != null)
            {
                // load from the path to this plugin assembly, not host executable
                string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
                string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
                dir = Path.Combine(dir, folder);

                var assmbPath = Path.Combine(dir, $"{argName}.dll");

                if (File.Exists(assmbPath))
                {
                    loadAssembly = Assembly.LoadFrom(assmbPath);
                }
                else
                {
                    throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
                }
            }

            return loadAssembly;
        }
    }
}