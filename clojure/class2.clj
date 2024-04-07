(defn our-partial [f & args] (fn [& more-args] (apply f (concat args more-args))))
((our-partial * 1 2) 3)

(apply * '(1 2) )
(defn f [& a] (print a))
(f 1)
(apply * '(1 2) '(1 2))

(((partial juxt #(str %) first)) ["apple" "banana" "cherry"])
(str ['apple])
((comp (partial take 3) reverse) (range 1 10))
(vector [1 2])
(#(* % %) 3 1 1)
((comp #(vector % %) (juxt even? odd?)) 7)

(def a (partial * ((partial identity))))

(a 3)
((comp * (partial identity)) 1)

(defmacro square [a]  (* a a))
(square 4)

(str "a" "b")

(source zero?)