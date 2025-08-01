using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace Chartboost.Mediation.BidMachine.Editor
{
    /// <summary>
    /// This Post-process Build script modifies the generated Xcode project for iOS builds.
    /// Specifically, it sets the "Other Linker Flags" (OTHER_LDFLAGS) for the main Unity-iPhone target
    /// to only include "-ObjC".
    /// </summary>
    public class BuildMachineBuildPostProcessor : IPostprocessBuildWithReport
    {
        /// <summary>
        /// Specifies the order in which post-process build callbacks are executed.
        /// A lower value means it runs earlier. We set it to a high value to run after most other
        /// build processes have completed, ensuring we can modify the final linker flags.
        /// </summary>
        public int callbackOrder => 999; // Run late to override other potential linker flags

        /// <summary>
        /// This method is called after the iOS build process is complete.
        /// It accesses the generated Xcode project and modifies its linker flags.
        /// </summary>
        /// <param name="report">A BuildReport object containing information about the build.</param>
        public void OnPostprocessBuild(BuildReport report)
        {
            // Ensure this script only runs for iOS builds.
            if (report.summary.platform != BuildTarget.iOS)
            {
                Debug.Log($"[BuildMachineBuildPostProcessor] Skipping post-process: Not an iOS build. Platform: {report.summary.platform}");
                return;
            }

            Debug.Log($"[BuildMachineBuildPostProcessor] Starting post-process for iOS build at: {report.summary.outputPath}");

            var projectPath = report.summary.outputPath;
            if (string.IsNullOrEmpty(projectPath))
            {
                Debug.LogError("[BuildMachineBuildPostProcessor] Could not find Xcode project path.");
                return;
            }

            // Create a new PBXProject instance and read the Xcode project file.
            var pbxProject = new PBXProject();
            var pbxProjectPath = PBXProject.GetPBXProjectPath(projectPath);
            pbxProject.ReadFromFile(pbxProjectPath);

            var targetGuid = pbxProject.GetUnityMainTargetGuid();
            if (string.IsNullOrEmpty(targetGuid))
            {
                Debug.LogError("[BuildMachineBuildPostProcessor] Could not find Unity Main Target GUID.");
                return;
            }

            Debug.Log($"[BuildMachineBuildPostProcessor] Modifying Linker Flags for target GUID: {targetGuid}");

            // Set the "Other Linker Flags" (OTHER_LDFLAGS) for the main target.
            // This will REPLACE any existing linker flags with just "-ObjC".
            // Required for BidMachine functionality !!
            pbxProject.SetBuildProperty(targetGuid, "OTHER_LDFLAGS", "-ObjC");
            Debug.Log("[BuildMachineBuildPostProcessor] Successfully set OTHER_LDFLAGS to '-ObjC' for the main Unity-iPhone target.");
            pbxProject.WriteToFile(pbxProjectPath);
        }
    }
}
