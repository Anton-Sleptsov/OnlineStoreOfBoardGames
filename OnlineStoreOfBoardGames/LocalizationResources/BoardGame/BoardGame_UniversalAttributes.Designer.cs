﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineStoreOfBoardGames.LocalizationResources.BoardGame {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class BoardGame_UniversalAttributes {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal BoardGame_UniversalAttributes() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PortalAboutEverything.LocalizationResources.BoardGame.BoardGame_UniversalAttribut" +
                            "es", typeof(BoardGame_UniversalAttributes).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
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
        ///   Ищет локализованную строку, похожую на characters.
        /// </summary>
        public static string TextInput_SymbolEndingFirstForm {
            get {
                return ResourceManager.GetString("TextInput_SymbolEndingFirstForm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на characters.
        /// </summary>
        public static string TextInput_SymbolEndingSecondForm {
            get {
                return ResourceManager.GetString("TextInput_SymbolEndingSecondForm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The value of the &quot;{0}&quot; field cannot be shorter than {1}  {2}.
        /// </summary>
        public static string TextInput_ValidationErrorMessageFew {
            get {
                return ResourceManager.GetString("TextInput_ValidationErrorMessageFew", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на The value of the field &quot;{0}&quot; cannot be longer than {1} {2}.
        /// </summary>
        public static string TextInput_ValidationErrorMessageMany {
            get {
                return ResourceManager.GetString("TextInput_ValidationErrorMessageMany", resourceCulture);
            }
        }
    }
}
